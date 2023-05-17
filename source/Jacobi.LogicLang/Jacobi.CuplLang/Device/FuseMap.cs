namespace Jacobi.CuplLang.Device;

internal struct FuseTable
{
    int ColumnWidth { get; }
    int CellWidth { get; }

    int[] InputPinTable { get; }
    int[] OutputPinTable { get; }
}

// calculates the fuse numbers for a device
// per mode a different fusemap (exceptions on common mapping)
// map input to macrocell/productterm
// 
// number offset for inputs (+ input delta)
// number offset for productterms (+ col width)
// number offset foldback output (+ foldback delta)
// output config fuses
// Product Term Disable (PTD) fuses
// User Electronic Signature (UES) fuses
// Device global mode fuses
internal sealed class FuseMap
{
    private readonly int _colWidth;
    private readonly int _cellWidth;

    public FuseMap(int colWidth, int cellWidth)
    {
        _colWidth = colWidth;
        _cellWidth = cellWidth;
    }

    public int FuseNumber(int inputPin, int outputPin, int productTerm)
    {
        // TODO: exceptions (pin 1)
        return inputPin + (productTerm * _colWidth) + (outputPin * _cellWidth);
    }
}
