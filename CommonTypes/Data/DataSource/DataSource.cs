using NoteManager.CommonTypes.Enums;

namespace NoteManager.CommonTypes.Data
{
    public class DataSource
    {
        private int _sourceID = -1;
        private string _sourceName = "";
        private NoteSourceType _noteSourceType = NoteSourceType.FromYoutube;
        private string _description = "";
        private MemoryStream? _data = null;

        /// <summary>
        /// Идентификатор источника.
        /// </summary>
        public int SourceID
        {
            get => _sourceID;
            set => _sourceID = value;
        }

        /// <summary>
        /// Наименование источника данных.
        /// </summary>
        public string SourceName
        {
            get => _sourceName;
            set => _sourceName = value;
        }

        /// <summary>
        /// Источник данных, на основе которых формируется заметка:
        /// книга в формате PDF или ссылка на Youtube видео.
        /// </summary>
        public NoteSourceType NoteSourceType
        {
            get => _noteSourceType;
            set => _noteSourceType = value;
        }

        /// <summary>
        /// Описание источника данных.
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = value;
        }

        /// <summary>
        /// Источник информации в бинарном виде. Пока задумывается и хранение ссылок на видео,
        /// так и PDF документ.
        /// </summary>
        public MemoryStream? Data
        {
            get => _data;
            set => _data = value;
        }



        public DataSource(int sourceID, string sourceName, NoteSourceType noteSourceType, string description)
        {
            _sourceID = sourceID;
            _sourceName = sourceName;
            _noteSourceType = noteSourceType;
            _description = description;
        }
    }
}
