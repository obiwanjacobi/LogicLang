using System.Collections.Generic;

namespace Jacobi.CuplLang.Device;

internal enum DeviceMode
{
    /// <summary>Device does not have a global mode.</summary>
    None,
    /// <summary>Device is in Simple mode.</summary>
    Simple,
    /// <summary>Device is using FlipFlops on the outputs.</summary>
    Registered,
    /// <summary>Device is in a combination of combinatorial and registered modes.</summary>
    Complex
}

internal enum OutputEnable
{
    /// <summary>Output Enable is not available.</summary>
    None,
    /// <summary>Output Enable is device global (usually a dedicated pin).</summary>
    Device,
    /// <summary>Output Enable is per MacroCell.</summary>
    MacroCell
}

internal abstract class Device
{
    protected Device()
    { }

    public DeviceMode Mode { get; protected set; }
    public virtual bool TrySetDeviceMode(DeviceMode mode)
    {
        return false;
    }

    public abstract IReadOnlyList<Pin> PinInfos { get; }
    public abstract IReadOnlyList<MacroCell> MacroCells { get; }
}
