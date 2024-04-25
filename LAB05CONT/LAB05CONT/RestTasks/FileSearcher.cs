using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileWatcher
{
    public class Search
    {
        public string directory;
        public string word;

        public Search(string directory_, string word_)
        {
            this.directory = directory_;
            this.word = word_;
        }

        public void Start()
        {
            Console.WriteLine("Searching in progress...");
            Thread thread = new Thread(() => SearchFile(directory, word));
            thread.Start();
        }

        public void SearchFile(string directory, string word)
        {
            foreach (var filePath in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(filePath).Contains(word))
                {
                    Console.WriteLine(filePath.Split(Path.DirectorySeparatorChar)[^1]);
                }
            }
        }
    }
}

