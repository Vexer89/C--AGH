using System;
using System.Collections.Generic;
using System.Threading;

namespace LAB05
{
    public class ProducerConsumer
    {
        public int Number { get; set; }
        public List<int> Data { get; set; }
        public Thread Thread = null;
        public int TimeSleep { get; set; }

        public ProducerConsumer(int number, List<int> data, int timeSleep)
        {
            Number = number;
            Data = data;
            TimeSleep = timeSleep;
        }
    }

    public class Producer : ProducerConsumer
    {
        public bool Running { get; set; }

        public Producer(int number, List<int> data, int timeSleep) : base(number, data, timeSleep)
        {
            Running = true;
        }

        public void Start()
        {
            while (Running)
            {
                Thread.Sleep(TimeSleep);
                WriteData();
            }
        }

        public void WriteData()
        {
            lock (Data)
            {
                Data.Add(Number);
            }
        }
    }

    public class Consumer : ProducerConsumer
    {
        public List<int> Consumed { get; set; }
        public Dictionary<int, int> ConsumedFromEachProducer { get; set; }
        public bool Running { get; set; }

        public Consumer(int number, List<int> data, int timeSleep) : base(number, data, timeSleep)
        {
            ConsumedFromEachProducer = new Dictionary<int, int>();
            Running = true;
        }

        public void Start()
        {
            while (Running)
            {
                Thread.Sleep(TimeSleep);
                GetData();
            }
            PrintConsumedData();
        }

        public void GetData()
        {
            lock (Data)
            {
                if (Data.Count == 0) return;
                int lastItem = Data[Data.Count - 1];
                if (ConsumedFromEachProducer.ContainsKey(lastItem))
                {
                    ConsumedFromEachProducer[lastItem]++;
                }
                else
                {
                    ConsumedFromEachProducer[lastItem] = 1;
                }
                Data.RemoveAt(Data.Count - 1);
            }
        }
        
        public void PrintConsumedData()
        {
            Console.WriteLine($"Consumer {Number} consumed data:");
            foreach (var i in ConsumedFromEachProducer)
            {
                Console.WriteLine($"Producer {i.Key} - {i.Value}");
            }
        }
    }
}