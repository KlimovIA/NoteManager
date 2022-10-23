﻿namespace EducationHelper.CommonTypes.Data
{
    public static class ItemIDManager
    {
        private static List<int> _itemIDList = new List<int>();        
        
        /// <summary>
        /// Выдаёт объекту свободный иденнтификационный номер.
        /// </summary>
        /// <returns></returns>
        public static int GetNewItemID()
        {  
            // Ищем свободный ID
            for (int i = 0; i < int.MaxValue; i++)
            {
                if (!_itemIDList.Contains(i))
                {
                    _itemIDList.Add(i);
                    return i;
                }
            }
            return -1; // Сюда не упадёт, но компилятор ругается, если этой строчки не будет.
        }

        public static void RemoveItemID(int itemID) => _itemIDList?.RemoveAt(_itemIDList.IndexOf(itemID));
        public static void AddItemID(int itemID) => _itemIDList?.Add(itemID);
    }
}
