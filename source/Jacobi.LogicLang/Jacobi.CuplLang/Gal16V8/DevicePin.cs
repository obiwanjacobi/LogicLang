using FuseNumber = System.Int32;

namespace Jacobi.CuplLang.Gal16V8;

internal class DevicePin
{
    public DevicePin(int pinNumber, PinCapability pinCaps)
    {
        Number = pinNumber;
        PinCapability = pinCaps;
    }

    public DevicePin(int pinNumber, PinCapability pinDir, FuseNumber fuseValue, FuseNumber fuseInverted)
        : this(pinNumber, pinDir)
    {
        ValueFuseBase = fuseValue;
        InvertedFuseBase = fuseInverted;
    }

    public int Number { get; }
    public PinCapability PinCapability { get; }
    public FuseNumber? ValueFuseBase { get; }
    public FuseNumber? InvertedFuseBase { get; }
}
