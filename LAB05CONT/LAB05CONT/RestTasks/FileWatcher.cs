using System;
using System.IO;
using System.Threading;

namespace FileWatcher
{
    public class FileWatch
    {
        public string path;
        public Thread thread;

        public FileWatch(string path_)
        {
            this.path = path_;
        }

        public void Start()
        {
            Console.WriteLine($"Monitoring directory: {path}");

            var watcher = new FileSystemWatcher(path);
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;
            watcher.Created += (sender, e) => Console.WriteLine($"Created: {e.Name}");
            watcher.Deleted += (sender, e) => Console.WriteLine($"Deleted: {e.Name}");

            thread = new Thread(() =>
            {
                Console.WriteLine("here");
                while (true)
                {
                    Console.WriteLine("Press 'q' to quit");
                    if (Console.ReadKey().Key == ConsoleKey.Q)
                    {
                        watcher.EnableRaisingEvents = false;
                        watcher.Dispose();
                        break;
                    }
                }
                Console.WriteLine("Stopped monitoring directory.");
            });
            thread.Start();
        }
    }
}