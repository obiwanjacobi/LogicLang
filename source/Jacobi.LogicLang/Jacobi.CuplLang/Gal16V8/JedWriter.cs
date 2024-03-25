﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jacobi.CuplLang.Gal16V8;

internal sealed class JedWriter
{
    private const string CopyRightComment = "Generated by CUPLlang v1.0 Jacobi Software (C) 2024";

    private readonly Stream _stream;
    private readonly TextWriter _writer;

    public JedWriter(Stream stream)
    {
        _stream = stream;
        _writer = new StreamWriter(stream);
    }

    public void Write(Placement placement)
    {
        WriteStx();
        WriteHeader(placement.DeviceName);
        WitePinCount(placement.PinCount);
        WriteFuseCount(placement.FuseCount);
        WriteProtect(placement.Protect);
        WriteDefaultValue();
        WriteNote(CopyRightComment);

        foreach (var line in placement.GetFuseLines())
        {
            WriteFuseLine(line);
        }

        WriteEtx();
    }

    private void WriteFuseLine(IEnumerable<Fuse> line)
    {
        var first = line.First();
        WriteLineAddress(first.Number);
        foreach (var fuse in line)
        {
            Write(fuse.Value);
        }
        WriteSeparator();
    }

    private void WriteLineAddress(int number)
    {
        Write("L");
        Write($"{number:D4}");
        Write(" ");
    }

    private void WriteHeader(string header)
    {
        Write(header);
        WriteSeparator();
    }

    private void WitePinCount(int pinCount)
    {
        Write("QP");
        Write(pinCount);
        WriteSeparator();
    }

    private void WriteFuseCount(int fuseCount)
    {
        Write("QF");
        Write(fuseCount);
        WriteSeparator();
    }

    private void WriteProtect(bool protect)
    {
        Write("G");
        Write(protect);
        WriteSeparator();
    }

    private void WriteDefaultValue()
    {
        Write("F0");
        WriteSeparator();
    }

    private void WriteNote(string note)
    {
        Write("N ");
        Write(note);
        WriteSeparator();
    }

    private void WriteStx()
        => WriteBinary(0x02);

    private void WriteEtx()
    {
        _writer.Flush();
        WriteBinary(0x03);
    }

    private void WriteBinary(int value)
    {
        Span<byte> buffer = stackalloc byte[1];
        buffer.Fill((byte)value);
        _stream.Write(buffer);
    }

    private void WriteSeparator()
        => _writer.WriteLine("*");

    private void Write(string value)
        => _writer.Write(value);

    private void Write(int value)
        => _writer.Write(value);

    private void Write(bool value)
        // reversed: 0 = connected, 1 = open
        => _writer.Write(value ? "0" : "1");
}
