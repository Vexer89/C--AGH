namespace LAB02;

public class RachunekBankowy
{
    private string numer;
    private decimal stanRachunku;
    private bool czyDozwolonyDebet;
    private List<PosiadaczRachunku> posiadaczeRachunku;
    List<Transakcja> _Transakcje = new List<Transakcja>();

    public string Numer
    {
        get { return numer; }
    }

    public decimal StanRachunku
    {
        get { return stanRachunku; }
    }
    

    public RachunekBankowy(string numer, decimal stanRachunku, bool czyDozwolonyDebet, List<PosiadaczRachunku> posiadaczeRachunku)
    {
        this.numer = numer;
        this.stanRachunku = stanRachunku;
        this.czyDozwolonyDebet = czyDozwolonyDebet;
        if (posiadaczeRachunku == null || posiadaczeRachunku.Count == 0)
        {
            throw new ArgumentException("Lista posiadaczy rachunku musi zawierać co najmniej jedną pozycję.");
        }

        this.posiadaczeRachunku = posiadaczeRachunku;
    }

    public static void DokonajTransakcji(RachunekBankowy rachunekZrodlowy, RachunekBankowy rachunekDocelowy, decimal kwota, string opis)
    {
        if (kwota < 0)
        {
            throw new ArgumentException("Kwota nie może być ujemna.");
        }

        if (rachunekDocelowy == null && rachunekZrodlowy == null)
        {
            throw new ArgumentException("Oba rachunki nie mogą być null.");
        }

        if (rachunekZrodlowy.czyDozwolonyDebet == false && rachunekZrodlowy.stanRachunku < kwota)
        {
            throw new ArgumentException(
                "Nie można dokonać transakcji. Brak wystarczających środków na rachunku źródłowym.");
        }

        if (rachunekZrodlowy != null)
        {
            rachunekZrodlowy.stanRachunku -= kwota;
        }

        if (rachunekDocelowy != null)
        {
            rachunekDocelowy.stanRachunku += kwota;
        }

        Transakcja transakcja = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);

        if (rachunekZrodlowy != null)
        {
            rachunekZrodlowy._Transakcje.Add(transakcja);
        }

        if (rachunekDocelowy != null)
        {
            rachunekDocelowy._Transakcje.Add(transakcja);
        }
    }

    public void DodajPosiadacza(PosiadaczRachunku posiadacz)
    {
        if (posiadaczeRachunku.Contains(posiadacz) || posiadacz == null)
        {
            throw new ArgumentException("Posiadacz nie moze zostac dodany.");
        }
        
        posiadaczeRachunku.Add(posiadacz);
    }

    public void UsunPosiadacza(PosiadaczRachunku posiadacz)
    {
        if (!posiadaczeRachunku.Contains(posiadacz) || posiadacz == null || posiadaczeRachunku.Count - 1 < 1)
        {
            throw new ArgumentException("Posiadacz nie moze zostac usuniety.");
        }

        posiadaczeRachunku.Remove(posiadacz);
    }

    public static RachunekBankowy operator +(RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        rachunek.DodajPosiadacza(posiadacz);
        return rachunek;
    }

    public static RachunekBankowy operator -(RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        rachunek.UsunPosiadacza(posiadacz);
        return rachunek;
    }

    public override string ToString()
    {
        string posiadacze = string.Join(", ", posiadaczeRachunku);
        string transakcje = string.Join(", ", _Transakcje);
        return $"Rachunek: Numer: {numer}, Stan rachunku: {stanRachunku}, Posiadacze: {{posiadacze}}, Transakcje: {{transakcje}}";
    }
}