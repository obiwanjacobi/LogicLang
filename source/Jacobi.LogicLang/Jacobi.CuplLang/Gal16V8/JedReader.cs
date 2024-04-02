using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jacobi.CuplLang.Gal16V8;

internal sealed class JedReader
{
    private readonly Stream _stream;
    private readonly TextReader _reader;
    private const char EOB = '*';
    private const byte STX = 0x02;
    private const byte ETX = 0x03;

    public JedReader(Stream stream)
    {
        _stream = stream;
        _reader = new StreamReader(stream);
    }

    public Placement Read()
    {
        Read(STX);
        var header = ReadBlock().Trim();

        var commands = new List<JedCommand>();
        var command = ReadCommand();
        while (command.Command > 0)
        {
            commands.Add(command);

            command = ReadCommand();
        }
        // The reader buffers the stream so Read on stream will not see the marker (it's in the reader's buffer)
        //Read(ETX);

        var placement = CreatePlacement(header, commands);
        return placement;
    }

    private Placement CreatePlacement(string header, List<JedCommand> commands)
    {
        var defaultFuseValue = commands.Single(cmd => cmd.Command == 'F').Data == "0";
        var protect = commands.Single(cmd => cmd.Command == 'G').Data == "1";
        var (pinCount, fuseCount) = GetPinAndFuseCount(commands.Where(cmd => cmd.Command == 'Q'));
        var fuses = CreateFuses(commands.Where(cmd => cmd.Command == 'L'));

        var placement = new Placement(header, pinCount, fuseCount, fuses);
        return placement;
    }

    private IReadOnlyList<Fuse> CreateFuses(IEnumerable<JedCommand> lCommands)
    {
        var fuses = new List<Fuse>();
        
        foreach (var cmdData in lCommands.Select(cmd => cmd.Data))
        {
            var index = cmdData.IndexOf(' ');
            var address = Int32.Parse(cmdData[0..index]);
            var fuseValues = cmdData[index..].Trim();

            for (var i = 0; i < fuseValues.Length; i++)
            {
                bool fuseValue = fuseValues[i] == '0';
                fuses.Add(new Fuse(address, fuseValue));
                address++;
            }
        }

        return fuses;
    }

    private (int pinCount, int fuseCount) GetPinAndFuseCount(IEnumerable<JedCommand> qCommands)
    {
        int pinCount = 0;
        int fuseCount = 0;

        foreach (var cmdData in qCommands.Select(cmd => cmd.Data))
        {
            if (cmdData.StartsWith('P'))
                pinCount = Int32.Parse(cmdData[1..]);
            if (cmdData.StartsWith('F'))
                fuseCount = Int32.Parse(cmdData[1..]);
        }

        return (pinCount, fuseCount);
    }

    private static List<char> _commands = [ 'C', 'D', 'F', 'G', 'K', 'L', 'N', 'P', 'Q' ];

    private JedCommand ReadCommand()
    {
        var block = ReadBlock();
        
        if (String.IsNullOrWhiteSpace(block))
            return new JedCommand();

        if (_commands.Contains(block[0]))
            return new JedCommand { Command = block[0], Data = block[1..] };
        
        return new JedCommand { Data = block };
    }

    private string ReadBlock()
    {
        StringBuilder block = new();
        Span<char> buffer = stackalloc char[1];

        while(_reader.Peek() is not ETX)
        {
            _reader.Read(buffer);
            if (buffer[0] is EOB)
                break;
            block.Append(buffer);
        }

        return block.ToString().Trim();
    }

    private void Read(int marker)
    {
        Span<byte> buffer = stackalloc byte[1];
        _stream.Read(buffer);

        if (buffer[0] != marker)
            throw new Exception($"Invalid JEDEC file. Cannot read the {marker} marker.");
    }

    //-------------------------------------------------------------------------

    private struct JedCommand
    {
        public char Command;
        public string Data;
    }
}
