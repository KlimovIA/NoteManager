using System.Reflection.Metadata.Ecma335;

namespace NoteManager.CommonTypes.Enums
{
    
    public enum ENodeType: byte
    { 
        /// <summary>
        /// Тип - папка, используется для хранения таких же папок и узлов.
        /// По уровню вложенности не ограничена.
        /// </summary>
        FolderNode,
        
        /// <summary>
        /// Тип - заметка, используется для хранения заметок по обозначенной области.
        /// По уровню вложенности ограничена - не может иметь в себе дочерних узлов.
        /// </summary>
        NoteNode,

        /// <summary>
        /// Корневой узел. Добавил сюда, чтобы повысить читабельность кода.
        /// По факту используется один раз.
        /// </summary>
        RootNode
    };
}
