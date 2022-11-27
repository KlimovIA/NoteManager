namespace NoteManager.CommonTypes.Data
{
    public static class ObjectDataComparison
    {
        public static int CompareObjectDataByParentID(ObjectData firstObj, ObjectData secondObj)
        {
            // Сравниваем ParentID. Первыми должны быть ParentID с меньшим значением.

            if (firstObj.ParentID == secondObj.ParentID) return   0;
            if (firstObj.ParentID > secondObj.ParentID)  return   1;
            if (firstObj.ParentID < secondObj.ParentID)  return  -1;
            return 0;
        }
    }
}
