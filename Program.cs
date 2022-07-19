using System.Text;
using BoogleAdnan;

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
    var readLine = Console.ReadLine();
    if (readLine != null && readLine.Length != 16)
    {
        GetInput();
    }
    return readLine;
}

int letterIndex = 0;

char[,] input = new char[4, 4];

for (int i = 0; i < 4; i++)
{
    for (int j = 0; j < 4; j++)
    {
        input[i, j] = letters[letterIndex];
        letterIndex++;
    }
    
}

FindWords(input, tree);
Console.WriteLine("Total 3 letter or more Words Found ------>" + totalWordsFound.ToString());
Console.Read();

void FindWords(char[,] boggle, Trie root)
{
    int m = boggle.GetLength(0);
    int n = boggle.GetLength(1);
    bool[,] visited = new bool[m, n];
    StringBuilder str = new StringBuilder();

    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < n; j++)
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

    var m = boggle.GetLength(0);
    var n = boggle.GetLength(1);

    if (IsSafe(m, n, i, j, visited))
    {
        visited[i, j] = true;

        foreach (var edge in child.Edges)
        {
            for (int row = i - 1; row <= i + 1; row++)
            {
                for (int col = j - 1; col <= j + 1; col++)
                {
                    if (IsSafe(m, n, row, col, visited) && boggle[row, col] == edge.Key)
                    {
                        SearchWord(edge.Value, boggle, row, col, visited, str.Append(edge.Key));
                    }
                }
            }

        }

        visited[i, j] = false;
    }
}

bool IsSafe(int m, int n, int i, int j, bool[,] visited)
{
    return i < m && i >= 0 && j < n && j >= 0 && !visited[i, j];
}



