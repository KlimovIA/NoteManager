using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.CommonTypes.Data
{
    internal static class DataSourceManager
    {
        private static List<DataSource> dataSources = new List<DataSource>();
        
        /// <summary>
        /// Список объектов, хранящих информацию об источниках данных
        /// </summary>
        public static List<DataSource> DataSources
        { 
            get => dataSources;
        }

        /// <summary>
        /// Убирает из всех объектов sourceID, который будет подвержен удалению.
        /// </summary>
        /// <param name="sourceID"> Идентификатор источника, который будет удаляться </param>
        public static void CleanSourceSelection(int sourceID)
        {
            var objects = ObjectDataManager.ObjectDataList.Where(x => x.DataSource?.SourceID == sourceID);
            foreach (var obj in objects)
                obj.DataSource.SourceID = -1;
        }

        /// <summary>
        /// Добавляет источник данных в общий список для взаимодействия.
        /// </summary>
        /// <param name="dataSource"> Ссылка на источик данных. </param>
        public static void AddDataSource(DataSource dataSource) => dataSources.Add(dataSource);
        
        /// <summary>
        /// Удаляет источник данных из общего списка для взаимодействия.
        /// </summary>
        /// <param name="dataSource"></param>
        public static void RemoveDataSource(DataSource dataSource) => dataSources.Remove(dataSource);
    }
}
