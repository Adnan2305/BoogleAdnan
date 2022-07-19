using System.Text;

Console.WriteLine("Reading file");
string[] wordlist = File.ReadAllLines(@"C:\Git\BoogleAdnan\safedict_full.txt");
Trie tree = new Trie();
foreach (var word in wordlist)
{
    tree.Insert(word);
}
string letters = GetInput();

int totalWordsFound = 0;

string GetInput()
{
    Console.WriteLine("Please enter 16 letters");
    string letters = Console.ReadLine();
    if (letters.Length != 16)
    {
        GetInput();
    }

    return letters;
}

int letterIndex = 0;

char[,] boggle = new char[4, 4];

for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        boggle[i, j] = letters[letterIndex];
        letterIndex++;
    }
    
}

FindWords(boggle, tree);
Console.WriteLine("Total Words Found ------>" + totalWordsFound.ToString());
Console.Read();

void FindWords(char[,] boggle, Trie root)
{
    int M = boggle.GetLength(0);
    int N = boggle.GetLength(1);
    bool[,] visited = new bool[M, N];
    StringBuilder str = new StringBuilder();

    for (int i = 0; i < M; i++)
    {
        for (int j = 0; j < N; j++)
        {
            //all the words start with one of the letters in the head of the Trie
            if (root.Root.Edges.ContainsKey(boggle[i, j]))
            {
                str.Append(boggle[i, j]);
                SearchWord(root.Root.Edges[boggle[i, j]], boggle, i, j, visited, str);
            }
            str.Clear();
        }
    }
}

void SearchWord(TrieNode child, char[,] boggle, int i, int j, bool[,] visited, StringBuilder str)
{
    if (child.IsTerminal && str.ToString().Length >= 3)
    {
        totalWordsFound++;
        Console.WriteLine(str.ToString());
    }

    int M = boggle.GetLength(0);
    int N = boggle.GetLength(1);

    if (IsSafe(M, N, i, j, visited))
    {
        visited[i, j] = true;

        foreach (var edge in child.Edges)
        {
            for (int row = i - 1; row <= i + 1; row++)
            {
                for (int col = j - 1; col <= j + 1; col++)
                {
                    if (IsSafe(M, N, row, col, visited) && boggle[row, col] == edge.Key)
                    {
                        SearchWord(edge.Value, boggle, row, col, visited, str.Append(edge.Key));
                    }
                }
            }

        }

        visited[i, j] = false;
    }
}

bool IsSafe(int M, int N, int i, int j, bool[,] visited)
{
    return i < M && i >= 0 && j < N && j >= 0 && !visited[i, j];
}

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

public class TrieNode
{
    public Dictionary<char, TrieNode> Edges { get; set; }
    public bool IsTerminal { get; set; }
    public TrieNode()
    {
        Edges = new Dictionary<char, TrieNode>();
    }
}