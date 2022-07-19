namespace BoogleAdnan
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Edges { get; set; }
        public bool IsTerminal { get; set; }
        public TrieNode()
        {
            Edges = new Dictionary<char, TrieNode>();
        }
    }
}
