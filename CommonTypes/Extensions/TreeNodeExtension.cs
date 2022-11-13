namespace NoteManager.CommonTypes.Data
{
    /// <summary>
    /// Методы расширения для TreeNode.
    /// </summary>
    public static class TreeNodeExtension
    {
        public static bool ContainsNode(this TreeNode node, TreeNode anotherNode)
        { 
            if (anotherNode.Parent is null) return false;
            if (anotherNode.Parent.Equals(node)) return true;
            return ContainsNode(node, anotherNode.Parent);
        }
    }
}
