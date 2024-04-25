using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;
using FileWatcher;

class Program
{

    void Ex2()
    {
        FileWatch fileWatcher = new FileWatch("C:\\Users\\krzys\\RiderProjects\\LAB05CONT\\LAB05CONT\\RestTasks");
        fileWatcher.Start();
    }

    void Ex3()
    {
        Search search = new Search("C:\\Users\\krzys\\RiderProjects\\LAB05CONT\\LAB05CONT\\RestTasks", "File");
        search.Start();
    }
    
    private static void Main(string[] args)
    {
        Program program = new Program();
        //program.Ex2();
        program.Ex3();
    }
}