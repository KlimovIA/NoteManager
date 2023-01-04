using NoteManager.CommonTypes.Enums;

namespace NoteManager.CommonTypes.Data
{

    public class ObjectData
    {
        private int _parentID;
        private int _objectID;
        private string _objectName;
        private readonly ENodeType _objectType;
        private int _dataSourceID;
        private DataSource? _dataSource;
        private MemoryStream? _noteText;
        private EDataStatus _dataStatus;

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
            set
            { 
                _parentID = value;
                if (_dataStatus != EDataStatus.DataAdd && _dataStatus != EDataStatus.DataDelete)
                    _dataStatus = EDataStatus.DataUpdate;
            }
        }

        /// <summary>
        /// Имя объекта
        /// </summary>
        public string ObjectName
        {
            get => _objectName;
            set
            {
                if (value is null) return;
                _objectName = value;
                if (_dataStatus != EDataStatus.DataAdd && _dataStatus != EDataStatus.DataDelete)
                    _dataStatus = EDataStatus.DataUpdate;                
            }
        }

        /// <summary>
        /// Тип объекта: папка или объект заметки.
        /// </summary>
        public ENodeType ObjectType
        {
            get => _objectType;
        }

        /// <summary>
        /// Идентификатор источника данных.
        /// </summary>
        public int SourceID
        {
            get => _dataSourceID;
            set 
            { 
                _dataSourceID = value;
                if (_dataStatus != EDataStatus.DataAdd && _dataStatus != EDataStatus.DataDelete)
                    _dataStatus = EDataStatus.DataUpdate;
            }
        }

        /// <summary>
        /// Текст заметки. Используем стрим, чтобы сохранять все шрифты в БД.
        /// </summary>
        public MemoryStream? Note
        {
            get => _noteText;
            set 
            {
                _noteText = value;
                if (_dataStatus != EDataStatus.DataAdd && _dataStatus != EDataStatus.DataDelete)
                    _dataStatus = EDataStatus.DataUpdate;
            }
        }

        /// <summary>
        /// Статус измения данных объекта.
        /// </summary>
        public EDataStatus DataStatus
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

        public ObjectData(int objectID, int parentID, string objectName, ENodeType objectType, int sourceID, MemoryStream? note)
        {
            _objectID = objectID;
            _parentID = parentID;
            _objectName = objectName;
            _objectType = objectType;
            _dataSourceID = sourceID;
            _noteText = note;
            _dataStatus = EDataStatus.DataNoneChange;
        }

        public ObjectData(int objectID, int parentID, string objectName, ENodeType objectType)
        {
            _objectID = objectID;
            _parentID = parentID;
            _objectName = objectName;
            _objectType = objectType;
            _dataStatus = EDataStatus.DataNoneChange;
            _dataSourceID = -1;
            _noteText = null;
        }
    }
}
