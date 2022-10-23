using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.CommonTypes.Data
{
    internal static class ObjectDataManager
    {
        private static readonly List<ObjectData> _objectDataList = new List<ObjectData>();

        public static List<ObjectData> ObjectDataList
        { 
            get => _objectDataList;
        }
    }
}
