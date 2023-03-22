namespace Jacobi.CuplLang
{
    public struct Location
    {
        public int Line;
        public int Col;
    }

    internal sealed class Diagnostic
    {
        public Diagnostic(int line, int col, string message)
        {
            Location = new Location { Line = line, Col = col };
            Message = message;
        }

        public Location Location { get; }
        public string Message { get; }
    }
}
