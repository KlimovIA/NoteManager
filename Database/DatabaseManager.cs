using NoteManager.CommonTypes.Data;
using NoteManager.CommonTypes.Enums;
using Microsoft.Data.Sqlite;
using System.Text;
using System.Xml.Linq;

namespace NoteManager.Database
{
    internal class DatabaseManager
    {
        // Пока не нашел метода добычи данных по названию поля в БД
        private enum NodesTableFields
        {
            ItemID,
            ParentItemID,
            ObjectName,
            ObjectTypeID,
            SourceID,
            NoteText
        }

        private enum DataSourceTableFields
        {
            ID,
            SourceName,
            SourceType,
            SourceDescription,
            SourceData
        }

        public delegate void DatabaseActionMessageHandler(string Message);
        public event DatabaseActionMessageHandler DatabaseActionEvent;
        public DatabaseManager()
        {
            // Если у нас не будет существовать файла БД, то создаём и набиваем его таблицами
            if (!File.Exists($"{Application.StartupPath}\\Database.db3"))
            {
                using (SqliteConnection _connection = new SqliteConnection("Data Source = Database.db3"))
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
                        NoteText TEXT)";
                        command.CommandType = System.Data.CommandType.Text;
                        command.ExecuteNonQuery();

                        // Создаём таблицу, которая будет хранить  источники данных в бинарном виде (PDF или ссылка на youtube) 
                        command.CommandText = @"CREATE TABLE DataSource(
                        ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                        SourceName TEXT NOT NULL,
                        SourceType INTEGER,
                        SourceDescription TEXT,
                        SourceData BLOB)";
                        command.CommandType = System.Data.CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    _connection.Close();
                }
            }
        }

        public void LoadObjectTreeFromDB()
        {
            // Заполним статический список, а в основной форме будем распаковывать
            using (SqliteConnection _connection = new SqliteConnection("Data Source = Database.db3"))
            {
                _connection.Open();

                using (SqliteCommand command = new SqliteCommand(null, _connection))
                {
                    // Создаём таблицу, хранящую узлы в нужном порядке и их объекты
                    command.CommandText = @"SELECT * FROM Nodes";
                    command.CommandType = System.Data.CommandType.Text;

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

                                ObjectType objectType = (ObjectType)reader.GetFieldValue<int>((int)NodesTableFields.ObjectTypeID);

                                string objectName = reader.GetFieldValue<string>((int)NodesTableFields.ObjectName);

                                int objectID = reader.GetFieldValue<int>((int)NodesTableFields.ItemID),
                                    parentObjectID = reader.GetFieldValue<int>((int)NodesTableFields.ParentItemID);

                                if (objectType == ObjectType.FolderNode)
                                {
                                    ObjectDataManager.ObjectDataList.Add(new ObjectData(objectID,
                                                                                      parentObjectID,
                                                                                      objectName,
                                                                                      objectType));
                                }
                                else
                                {
                                    int sourceID = reader.GetFieldValue<int>((int)NodesTableFields.SourceID);
                                    // Сюда докидываем недостающие поля для заметки
                                    ObjectDataManager.ObjectDataList.Add(new ObjectData(objectID,
                                                                                      parentObjectID,
                                                                                      objectName,
                                                                                      objectType,
                                                                                      sourceID,
                                                                                      new StringBuilder().Append(reader.GetFieldValue<string>((int)NodesTableFields.NoteText))));
                                }
                                // Добавляем в хранилище ID номер из базы
                                ItemIDManager.AddItemID(reader.GetFieldValue<int>((int)NodesTableFields.ItemID));                                                               
                            }
                        }                      
                    }
                }
                _connection.Close();
            }           
        }

        public bool UpdateObjectsInDatabase()
        {
            try
            {
                foreach (ObjectData objData in ObjectDataManager.ObjectDataList)
                {
                    switch (objData.DataStatus)
                    {
                        case DataStatus.DataAdd:
                            AddNodeToDB(objData);
                            break;

                        case DataStatus.DataUpdate:
                            UpdateNodeInDB(objData);
                            break;

                        case DataStatus.DataDelete:
                            DeleteNodeFromDB(objData);
                            break;

                        case DataStatus.DataNoneChange:
                            break;
                    };
                }

                
                // Пробежались по объектам, теперь будем выбрасывать все объекты, что были помечены на удаление
                var RemovedDataObjects = ObjectDataManager.ObjectDataList.Where(x => x.DataStatus == DataStatus.DataDelete);
                
                // Удаляем объект из списка объектов, чтобы сборщик мусора его выкинул из памяти.                   
                while (RemovedDataObjects.Count() > 0)
                    ObjectDataManager.ObjectDataList.Remove(RemovedDataObjects.First());
            }
            catch (Exception)
            {
                //throw new Exception("Ошибка выполнения запроса к базе данных!");
                MessageBox.Show("Ошибка выполнения запроса к базе данных!", "Ошибка выполнения!");
                RaiseDatabaseActionEvent("Модификация данных не выполнена.");
                return false;
            }
            RaiseDatabaseActionEvent("Модификация данных в базе данных прошла успешно.");
            return true;
        }

        /// <summary>
        /// Сохранение узла дерева в БД. Узел дерева, помеченный флагом DataAdd,
        /// будет добавлен в БД.
        /// </summary>
        /// <param name="node">Узел дерева</param>
        private void AddNodeToDB(ObjectData objData)
        {
            // Сохраняем узел
            int objectID = objData.ObjectID;
            int parentObjectID = objData.ParentID;
            string objectName = objData.ObjectName;
            int objectType = (int)objData.ObjectType;
            int sourceID = objData.SourceID;
            string noteText = objData.Note?.ToString()?? "";

            using (SqliteConnection _connection = new SqliteConnection("Data Source = Database.db3"))
            {
                _connection.Open();

                using (SqliteCommand command = new SqliteCommand(null, _connection))
                {
                    // Добавляем объект в БД
                    command.CommandText = @"INSERT INTO Nodes (ItemID, ParentItemID, ObjectName, ObjectTypeID, SourceID, NoteText) 
                                            VALUES (@itemID, @parentObjectID, @objectName, @objectTypeID, @sourceID, @noteText)";

                    command.Parameters.Add(new SqliteParameter("@itemID", objectID));
                    command.Parameters.Add(new SqliteParameter("@parentObjectID", parentObjectID));
                    command.Parameters.Add(new SqliteParameter("@objectName", objectName));
                    command.Parameters.Add(new SqliteParameter("@objectTypeID", objectType));
                    command.Parameters.Add(new SqliteParameter("@sourceID", sourceID));
                    command.Parameters.Add(new SqliteParameter("@noteText", noteText));

                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();

                    // Удаляем номер из списка идентификаторов
                    ItemIDManager.RemoveItemID(objectID);
                }
                _connection.Close();
            }
            // Помечаем, что работа с этим объектом завершена и больше повторять эти действия не надо будет.
            objData.DataStatus = DataStatus.DataNoneChange;
        }

        private void DeleteNodeFromDB(ObjectData objData)
        {
            int objectID = objData.ObjectID;
            using (SqliteConnection _connection = new SqliteConnection("Data Source = Database.db3"))
            {
                _connection.Open();

                using (SqliteCommand command = new SqliteCommand(null, _connection))
                {
                    // Удаляем объект из БД
                    command.CommandText = @$"DELETE FROM Nodes 
                                            WHERE ItemID ={objectID}";
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();

                    // Удаляем номер из списка идентификаторов                   
                    ItemIDManager.RemoveItemID(objectID);
                }
                _connection.Close();
            }
        }

        private void UpdateNodeInDB(ObjectData objData)
        {
            int objectID = objData.ObjectID;
            int parentID = objData.ParentID;
            int sourceID = objData.SourceID;
            string objectName = objData.ObjectName;
            string noteText = objData.Note?.ToString() ?? "";

            using (SqliteConnection _connection = new SqliteConnection("Data Source = Database.db3"))
            {
                _connection.Open();

                using (SqliteCommand command = new SqliteCommand(null, _connection))
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
                    command.Parameters.Add(new SqliteParameter("@noteText", noteText));
                    command.Parameters.Add(new SqliteParameter("@objectID", objectID));

                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();                   
                }
                _connection.Close();
            }
            objData.DataStatus = DataStatus.DataNoneChange;
        }
        private void RaiseDatabaseActionEvent(string Message) => DatabaseActionEvent?.Invoke(Message);
    }
}
