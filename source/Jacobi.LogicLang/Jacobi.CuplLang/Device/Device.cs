using System;
using System.Collections.Generic;
using System.Linq;
using Jacobi.CuplLang.Ast;
using Jacobi.CuplLang.Device.Gal16V8;

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

    public const string G16V8 = "G16V8";
    public const string G22V10 = "G22V10";

    public static Device Create(string device)
    {
        var name = device.ToUpper();

        if (name.StartsWith(G16V8))
        {
            return new G16V8();
        }

        //if (name.StartsWith(G22V10))
        //{
        //    return new G22V10();
        //}

        throw new ArgumentException($"The device '{device}' is not supported.", nameof(device));
    }

    public DeviceMode Mode { get; protected set; }
    public virtual bool TrySetDeviceMode(DeviceMode mode)
    {
        return false;
    }

    public abstract IReadOnlyList<Pin> Pins { get; }
    public abstract IReadOnlyList<MacroCell> MacroCells { get; }
}
