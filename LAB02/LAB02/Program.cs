// See https://aka.ms/new-console-template for more information

using System;

using LAB02;

class Program
{
    static void Main(string[] args)
    {
        OsobaFizyczna wlasciciel1 = new OsobaFizyczna("Anna", "Nowak", "Maja", "98765432111", "2345");
        OsobaFizyczna wlasciciel2 = new OsobaFizyczna("Anna", "Nowak", "Maja", "98765432111", "2345");
        
        List<PosiadaczRachunku> posiadacze = new List<PosiadaczRachunku>();
        
        posiadacze.Add(wlasciciel1);
        posiadacze.Add(wlasciciel2);
        
        RachunekBankowy rachunekA = new RachunekBankowy("123456789", 1000, true, posiadacze);
        RachunekBankowy rachunekB = new RachunekBankowy("987654321", 500, false, posiadacze);
        
        OsobaFizyczna wlascicielA = new OsobaFizyczna("Jan", "Kowalski", "Adam", "98765432100", "1345");
        OsobaFizyczna wlascicielB = new OsobaFizyczna("Anna", "Nowak", "Maja", "9876543", "2345");
        
        rachunekA += wlascicielA;
        rachunekB += wlascicielB;
        
        Transakcja transakcja1 = new Transakcja(rachunekA, rachunekB, 200, "Przelew");
        //Transakcja transakcja2 = new Transakcja(null, rachunekA, 300, "Wpłata gotówkowa");
        //Transakcja transakcja3 = new Transakcja(rachunekB, null, 150, "Wypłata gotówkowa");
        
        Console.WriteLine(transakcja1.ToString());
        //Console.WriteLine(transakcja2.ToString());
        //Console.WriteLine(transakcja3.ToString());
        //Console.WriteLine(rachunekA.ToString());
    }
}