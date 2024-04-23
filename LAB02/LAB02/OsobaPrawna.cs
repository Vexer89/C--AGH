namespace LAB02;

public class OsobaPrawna : PosiadaczRachunku
{
    private string nazwa;
    private string siedziba;

    public string Nazwa
    {
        get { return nazwa; }
    }

    public string Siedziba
    {
        get { return siedziba; }
    }

    public override string ToString()
    {
        return $"Osoba prawna: {nazwa}, siedziba: {siedziba}";
    }

    public OsobaPrawna(string nazwa, string siedziba)
    {
        this.nazwa = nazwa;
        this.siedziba = siedziba;
    }
}