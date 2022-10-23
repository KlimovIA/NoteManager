namespace NoteManager.CommonTypes.Enums
{
    
    public enum NoteSourceType: byte
    {
        /// <summary>
        /// Источник данных - документ PDF.
        /// *** Проверить возможность сохранения PDF документа в BLOB поле БД.
        /// </summary>
        FromPDF,
        
        /// <summary>
        /// Источник данных - ссылка на видео из youtube.
        /// *** Предполагается открытие проигрывателя, который будет работать по ссылке.
        /// </summary>
        FromYoutube
    };  
}
