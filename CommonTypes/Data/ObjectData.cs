using EducationHelper.CommonTypes.Enums;
using System.Text;

namespace EducationHelper.CommonTypes.Data
{

    public class ObjectData
    {
        private int _parentID;
        private int _objectID;
        private string _objectName = "";
        private readonly ObjectType _objectType;
        private int _dataSourceID;
        private DataSource? _dataSource;
        private readonly StringBuilder? _noteText;
        private DataStatus _dataStatus;              

        /// <summary>
        /// ID узла. Применяется для взаимосвязи в дереве и для использования в БД.
        /// </summary>
        public int ObjectID
        { 
            get => _objectID;
            set => _objectID = value;   
        }

        /// <summary>
        /// ID родительского узла. Для постройки дерева объектов.
        /// </summary>
        public int ParentID
        {
            get => _parentID;
            set => _parentID = value;
        }

        /// <summary>
        /// Имя объекта
        /// </summary>
        public string ObjectName
        {
            get => _objectName;
            set => _objectName = value;
        }

        /// <summary>
        /// Тип объекта: папка или объект заметки.
        /// </summary>
        public ObjectType ObjectType
        {
            get => _objectType;
        }

        /// <summary>
        /// Идентификатор источника данных.
        /// </summary>
        public int SourceID
        {
            get => _dataSourceID;
            set => _dataSourceID = value;
        }

        /// <summary>
        /// Текст заметки.
        /// </summary>
        public StringBuilder? Note
        {
            get => _noteText;
        }

        /// <summary>
        /// Статус измения данных объекта.
        /// </summary>
        public DataStatus DataStatus
        {
            get => _dataStatus;
            set => _dataStatus = value;
        }

        /// <summary>
        /// Ссылка на источник данных.
        /// </summary>
        public DataSource? DataSource
        { 
            get => _dataSource;
            set => _dataSource = value;
        }

        public ObjectData(int objectID, int parentID, string objectName, ObjectType objectType, int sourceID, StringBuilder? note)
        {
            _objectID = objectID;
            _parentID = parentID;
            _objectName = objectName;
            _objectType = objectType;
            _dataSourceID = sourceID;
            _noteText = note;
            _dataStatus = DataStatus.DataNoneChange;
        }

        public ObjectData(int objectID, int parentID, string objectName, ObjectType objectType)
        {           
            _objectID = objectID;
            _parentID = parentID;
            _objectName = objectName;
            _objectType = objectType;
            _dataStatus = DataStatus.DataNoneChange;
            _dataSourceID = -1;
            _noteText = null;
        }
    }
}
