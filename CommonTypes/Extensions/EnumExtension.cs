using NoteManager.CommonTypes.Enums;

namespace NoteManager.CommonTypes.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt(this EDataStatus dataStatus) => (int)dataStatus;
        public static int ToInt(this ENodeType nodeType) => (int)nodeType;
        public static int ToInt(this ENoteSourceType noteSourceType) => (int)noteSourceType;
    }
}
