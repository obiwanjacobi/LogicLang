namespace Jacobi.CuplLang.Gal16V8;

internal sealed class PinReference
{
    public PinReference(int pinNumber, PinMode mode)
    {
        PinNumber = pinNumber;
        Mode = mode;
    }

    public int PinNumber { get; }
    public PinMode Mode { get; }
}
