namespace Jacobi.CuplLang;

internal struct Location
{
    public int Line;
    public int Col;

    public override string ToString()
        => $"{Line}:{Col}";
}

internal sealed class Diagnostic
{
    public Diagnostic(string message)
    {
        Message = message;
    }

    public Diagnostic(int line, int col, string message)
    {
        Location = new Location { Line = line, Col = col };
        Message = message;
    }

    public Location Location { get; }
    public string Message { get; }

    public override string ToString()
        => $"{Message} ({Location})";
}
