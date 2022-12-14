using NoteManager.CommonTypes.Data;
using NoteManager.CommonTypes.Enums;
using Microsoft.Data.Sqlite;
using System.Text;
using System.Data;
using System.ComponentModel;
using NoteManager.CommonTypes.Data.Debug;

namespace NoteManager.Database
{
    public static class DatabaseManager
    {
        private static readonly string _dataSourceConnectionString = "Data Source = Database.db3";
        private static SqliteConnection _connection;
        private static SqliteTransaction? _transaction;
        static DatabaseManager()
        {
            _connection = new SqliteConnection(_dataSourceConnectionString);
            // Если у нас не будет существовать файла БД, то создаём и набиваем его таблицами
            if (!File.Exists($"{Application.StartupPath}\\Database.db3"))
            {
                using (SqliteConnection _connection = new SqliteConnection(_dataSourceConnectionString))
                {
                    _connection.Open();
                    using (SqliteCommand command = new SqliteCommand("", _connection))
                    {
                        // Создаём таблицу, хранящую узлы в нужном порядке и их объекты
                        command.CommandText = @"CREATE TABLE Nodes(                       
                                                                   ItemID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                                   ParentItemID INTEGER NOT NULL,
                                                                   ObjectName TEXT NOT NULL,
                                                                   ObjectTypeID INTEGER NOT NULL,
                                                                   SourceID INTEGER NOT NULL,
                                                                   NoteText BLOB)";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();

                        // Создаём таблицу, которая будет хранить  источники данных в бинарном виде (PDF или ссылка на youtube) 
                        command.CommandText = @"CREATE TABLE DataSource(
                                                                        ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                                        SourceName TEXT NOT NULL,
                                                                        SourceType INTEGER,
                                                                        SourceDescription TEXT,
                                                                        SourceData BLOB)";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    _connection.Close();
                }
            }
        }

        public static void LoadObjectTreeFromDB()
        {
            // Заполним статический список, а в основной форме будем распаковывать
            using (SqliteConnection _connection = new SqliteConnection(_dataSourceConnectionString))
            {
                _connection.Open();

                using (SqliteCommand command = new SqliteCommand(null, _connection))
                {
                    // Создаём таблицу, хранящую узлы в нужном порядке и их объекты
                    command.CommandText = @"SELECT * FROM Nodes";
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                /// Делим объекты по типам:
                                /// 1. Если у нас объект - папка, то у него минимум параметров. 
                                ///    Имя и ссылка на родителя. Плюс его идетификатор.
                                /// 2. Если объект - заметка, то параметров берется по максимуму.

                                ENodeType objectType = (ENodeType)reader.GetFieldValue<int>("ObjectTypeID");

                                string objectName = reader.GetFieldValue<string>("ObjectName");

                                int objectID = reader.GetFieldValue<int>("ItemID"),
                                    parentObjectID = reader.GetFieldValue<int>("ParentItemID");

                                if (objectType == ENodeType.FolderNode)
                                {
                                    ObjectDataManager.ObjectDataList.Add(new ObjectData(objectID,
                                                                                        parentObjectID,
                                                                                        objectName,
                                                                                        objectType));
                                }
                                else
                                {
                                    int sourceID = reader.GetFieldValue<int>("SourceID");
                                    // Сюда докидываем недостающие поля для заметки
                                    ObjectDataManager.ObjectDataList.Add(new ObjectData(objectID,
                                                                                        parentObjectID,
                                                                                        objectName,
                                                                                        objectType,
                                                                                        sourceID,
                                                                                        new MemoryStream(reader.GetFieldValue<byte[]>("NoteText"))));
                                }
                                // Добавляем в хранилище ID номер из базы
                                ItemIDManager.AddItemID(reader.GetFieldValue<int>("ItemID"));
                            }
                        }
                    }
                }
                _connection.Close();
            }
        }

        public static bool SaveToDataBase()
        {
            try
            {
                _connection.Open();
                using (_transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        foreach (ObjectData objData in ObjectDataManager.ObjectDataList)
                        {
                            switch (objData.DataStatus)
                            {
                                case EDataStatus.DataAdd:
                                    AddNodeToDB(objData);
                                    break;

                                case EDataStatus.DataUpdate:
                                    UpdateNodeInDB(objData);
                                    break;

                                case EDataStatus.DataDelete:
                                    DeleteNodeFromDB(objData);
                                    break;

                                case EDataStatus.DataNoneChange:
                                    break;
                            };
                        }

                        // Пробежались по объектам, теперь будем выбрасывать все объекты, что были помечены на удаление
                        var RemovedDataObjects = ObjectDataManager.ObjectDataList.Where(x => x.DataStatus == EDataStatus.DataDelete);

                        // Удаляем объект из списка объектов, чтобы сборщик мусора его выкинул из памяти.                   
                        while (RemovedDataObjects.Count() > 0)
                            ObjectDataManager.ObjectDataList.Remove(RemovedDataObjects.First());
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        Logger.WriteLogMessage("DatabaseManager", e.Message);
#endif
                        ActionNotification.ShowActionNotification(Constants.DataModificationNotDone, ActionNotification.NotificationResultType.ResultError);
                        _transaction.Rollback();
                        return false;
                    }
                    ActionNotification.ShowActionNotification(Constants.DataModificationSuccess, ActionNotification.NotificationResultType.ResultOK);
                    _transaction.Commit();
                    return true;
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Сохранение узла дерева в БД. Узел дерева, помеченный флагом DataAdd,
        /// будет добавлен в БД.
        /// </summary>
        /// <param name="node">Узел дерева</param>
        private static void AddNodeToDB(ObjectData objData)
        {
            // Сохраняем узел
            int objectID = objData.ObjectID;
            int parentObjectID = objData.ParentID;
            string objectName = objData.ObjectName;
            int objectType = (int)objData.ObjectType;
            int sourceID = objData.SourceID;
            byte[]? noteText = objData.Note?.ToArray();

            using (SqliteCommand command = new SqliteCommand(null, _connection, _transaction))
            {
                // Добавляем объект в БД
                command.CommandText = @"INSERT INTO Nodes (ItemID,ParentItemID, ObjectName, ObjectTypeID, SourceID, NoteText) 
                                                   VALUES (@itemID, @parentObjectID, @objectName, @objectTypeID, @sourceID, @noteText)";

                command.Parameters.Add(new SqliteParameter("@itemID", objectID));
                command.Parameters.Add(new SqliteParameter("@parentObjectID", parentObjectID));
                command.Parameters.Add(new SqliteParameter("@objectName", objectName));
                command.Parameters.Add(new SqliteParameter("@objectTypeID", objectType));
                command.Parameters.Add(new SqliteParameter("@sourceID", sourceID));
                command.Parameters.Add(new SqliteParameter("@noteText", noteText is null ? "NULL" : noteText));

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            // Помечаем, что работа с этим объектом завершена и больше повторять эти действия не надо будет.
            objData.DataStatus = EDataStatus.DataNoneChange;
        }

        private static void DeleteNodeFromDB(ObjectData objData)
        {
            int objectID = objData.ObjectID;

            using (SqliteCommand command = new SqliteCommand(null, _connection, _transaction))
            {
                // Удаляем объект из БД
                command.CommandText = @$"DELETE FROM Nodes 
                                         WHERE ItemID ={objectID}";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                // Удаляем номер из списка идентификаторов                   
                ItemIDManager.RemoveItemID(objectID);
            }
        }

        private static void UpdateNodeInDB(ObjectData objData)
        {
            int objectID = objData.ObjectID;
            int parentID = objData.ParentID;
            int sourceID = objData.SourceID;
            string objectName = objData.ObjectName;
            byte[]? noteText = objData.Note?.ToArray();

            using (SqliteCommand command = new SqliteCommand(null, _connection, _transaction))
            {
                // Обновляем данные в БД
                command.CommandText = @"UPDATE Nodes
                                        SET ParentItemID = @parentObjectID,
                                            ObjectName = @objectName,
                                            SourceID = @sourceID,
                                            NoteText = @noteText
                                        WHERE ItemID = @objectID";

                command.Parameters.Add(new SqliteParameter("@parentObjectID", parentID));
                command.Parameters.Add(new SqliteParameter("@objectName", objectName));
                command.Parameters.Add(new SqliteParameter("@sourceID", sourceID));
                command.Parameters.Add(new SqliteParameter("@noteText", noteText is null ? "NULL" : noteText));
                command.Parameters.Add(new SqliteParameter("@objectID", objectID));

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            // Помечаем, что работа с этим объектом завершена и больше повторять эти действия не надо будет.
            objData.DataStatus = EDataStatus.DataNoneChange;
        }
    }
}
