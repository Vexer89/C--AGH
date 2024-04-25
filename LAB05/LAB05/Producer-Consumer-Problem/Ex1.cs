using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB05
{
    public class Ex1
    {
        Random random = new Random();
        List<Producer> producers = new List<Producer>();
        List<Consumer> consumers = new List<Consumer>();
        List<int> data = new List<int>();
        int m;
        int n;

        public Ex1(int m_, int n_)
        {
            this.m = m_;
            this.n = n_;
        }

        public void Start()
        {
            for (int i =  0; i < m; i++)
            {
                Consumer consumer = new Consumer(i, data, random.Next(100, 1000));
                consumer.Thread = new Thread(new ThreadStart(consumer.Start));
                consumers.Add(consumer);
            }
            for (int i =  0; i < n; i++)
            {
                Producer producer = new Producer(i, data, random.Next(200, 2000));
                producer.Thread = new Thread(new ThreadStart(producer.Start));
                producers.Add(producer);
            }
            foreach ( var i in producers)
            {
                i.Thread.Start();
                Console.WriteLine("Producer " + i.Number + " started");
            }
            foreach ( var i in consumers)
            {
                i.Thread.Start();
                Console.WriteLine("Consumer " + i.Number + " started");
            }
            
            Console.WriteLine("Press 'q' to quit.");
            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
                Console.WriteLine("Press 'q' to quit.");
            }
            Stop();
        }

        public void Stop()
        {
            foreach (var i in producers)
            {
                i.Running = false;
                Console.WriteLine("Producer " + i.Number + " stopped");
            }
            foreach (var i in consumers)
            {
                i.Running = false;
                Console.WriteLine("Consumer " + i.Number + " stopped");
            }
        }
    }
}

