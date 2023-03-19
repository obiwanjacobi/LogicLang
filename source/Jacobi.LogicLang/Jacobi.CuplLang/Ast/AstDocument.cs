namespace Jacobi.CuplLang.Ast;

internal class AstDocument
{
    public  AstHeader Header { get; set; } = new AstHeader();

    public IReadOnlyList<AstPin> Pins { get; set; } = new List<AstPin>();

    public IReadOnlyList<AstEquation> Equations { get; set; } = new List<AstEquation>();
}
