namespace BoogleAdnan
{
    public class Trie
    {
        public TrieNode Root { get; set; }
        public Trie()
        {
            Root = new TrieNode();
        }

        public void Insert(string word)
        {
            var current = Root;
            for (int i = 0; i < word.Length; i++)
            {
                if (!current.Edges.ContainsKey(word[i]))
                {
                    current.Edges.Add(word[i], new TrieNode());
                }
                current = current.Edges[word[i]];
            }
            current.IsTerminal = true;
        }
    }
}


