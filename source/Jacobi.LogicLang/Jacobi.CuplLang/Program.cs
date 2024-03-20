using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Threading.Tasks;

namespace Jacobi.CuplLang;

public static class Program
{
    private readonly static Output _output = new();

    public static async Task<int> Main(string[] args)
    {
        _output.WriteLine("-----------------------------");
        _output.WriteLine("CUPL Language Compiler v1.0.0");
        _output.WriteLine("  Jacobi Software (c) 2024");
        _output.WriteLine("-----------------------------");

        var rootCmd = CreateCommandLineCommands();
        return await rootCmd.InvokeAsync(args);
    }

    private static async Task CompileAsync(FileInfo file, string? output)
    {
        if (!file.Exists)
        {
            _output.Error($"Cannot open file: '{file.FullName}'.");
            return;
        }

        using var reader = file.OpenText();
        var compiler = new Compiler();
        var fileCtx = compiler.Parse(await reader.ReadToEndAsync(), file.Name);
        if (DisplayDiagnostics(compiler.Diagnostics)) return;
        _output.WriteLine("Parsing completed.");

        var astDoc = compiler.BuildAst(fileCtx);
        if (DisplayDiagnostics(compiler.Diagnostics)) return;
        _output.WriteLine("Abstraction completed.");

        var placement = compiler.DoPlacement(astDoc);
        if (DisplayDiagnostics(compiler.Diagnostics)) return;
        _output.WriteLine("Placement completed.");

        if (String.IsNullOrEmpty(output))
            output = Path.Combine(file.DirectoryName!,
                $"{Path.GetFileNameWithoutExtension(file.Name)}.jed");

        compiler.GenerateJedec(placement, output);
        if (DisplayDiagnostics(compiler.Diagnostics)) return;
        _output.WriteLine($"'{output}' generated.");
    }

    private static bool DisplayDiagnostics(IReadOnlyCollection<Diagnostic> diagnostics)
    {
        foreach (var diag in diagnostics)
        {
            _output.Error(diag.ToString());
        }
        return diagnostics.Count > 0;
    }

    private static RootCommand CreateCommandLineCommands()
    {
        var inputOption = new Option<FileInfo>(
            new[] { "--file", "-f" },
            "The location and name of the input file.")
        {
            IsRequired = true
        };
        var outputOption = new Option<string>(
            new[] { "--out-path", "-o" },
            "The location and name of the output file.");
        var rootCmd = new RootCommand("Compiles a CUPL .pld input file into .jed output.")
        {
            inputOption,
            outputOption
        };

        rootCmd.SetHandler(CompileAsync, inputOption, outputOption);

        return rootCmd;
    }
}

internal class Output
{
    public void WriteLine()
        => Console.WriteLine();

    public void WriteLine(string text)
        => Console.WriteLine(text);

    public void Error(string message)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {message}");
        Console.ForegroundColor = color;
    }
}