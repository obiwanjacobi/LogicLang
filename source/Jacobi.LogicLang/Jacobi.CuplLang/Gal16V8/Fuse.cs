using FuseNumber = System.Int32;

namespace Jacobi.CuplLang.Gal16V8;

internal record Fuse
{
    public Fuse(FuseNumber number, bool value = true)
    {
        Number = number;
        Value = value;
    }

    public FuseNumber Number { get; }
    public bool Value { get; }
}
