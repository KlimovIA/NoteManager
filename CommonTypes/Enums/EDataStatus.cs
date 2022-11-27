namespace NoteManager.CommonTypes.Enums
{
    /// <summary>
    /// Статус данных.
    /// </summary>
    public enum EDataStatus: byte
    {
        /// <summary>
        /// Данные без изменений. Этот статус приобретают те объекты, которые были
        /// загружены из БД и не подвергались никаким изменениям.
        /// </summary>
        DataNoneChange,
        
        /// <summary>
        /// Добавление данных в БД
        /// </summary>
        DataAdd,
        
        /// <summary>
        /// Обновление данных в БД
        /// </summary>
        DataUpdate,
        
        /// <summary>
        /// Удаление данных из БД
        /// </summary>
        DataDelete
    }
}
