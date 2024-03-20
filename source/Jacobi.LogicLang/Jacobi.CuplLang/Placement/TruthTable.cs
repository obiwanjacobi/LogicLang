using System.Collections.Generic;
using System.Text;
using Jacobi.CuplLang.Ast;

namespace Jacobi.CuplLang.Placement;

internal sealed class TruthTable
{
    private readonly Evaluator _evaluator;
    private readonly List<TruthTableItem> _items = new();

    public TruthTable(IEnumerable<AstEquation> equations)
    {
        _evaluator = new Evaluator(equations);
        Initialize();
    }

    private void Initialize()
    {
        var inputValue = 0;
        var inputs = _evaluator.NewInputs();
        var keepGoing = true;

        while (keepGoing)
        {
            var outputs = _evaluator.Eval(inputs);
            _items.Add(new TruthTableItem(inputs.Values, outputs.Values));

            inputValue++;
            keepGoing = TryBuildInputs(inputValue, inputs);
        }
    }

    private static bool TryBuildInputs(int value, Dictionary<string, bool> inputs)
    {
        if ((value & (1 << inputs.Count)) > 0) return false;

        var mask = 1;

        foreach (var kvp in inputs)
        {
            inputs[kvp.Key] = (value & mask) > 0;
            mask <<= 1;
        }

        return true;
    }
    
    public override string ToString()
    {
        var builder = new StringBuilder();
        var widths = new List<int>();

        // header
        foreach (var input in _evaluator.Inputs)
        {
            builder.Append("| ")
                .Append(input).
                Append(' ');
            widths.Add(input.Length + 2);
        }
        builder.Append('|');
        widths.Add(0);
        foreach (var output in _evaluator.Outputs)
        {
            builder.Append("| ")
                .Append(output)
                .Append(' ');
            widths.Add(output.Length + 2);
        }
        builder.AppendLine("|");

        // separator
        foreach (var w in widths)
        {
            builder.Append('|')
                .Append(new string('-', w));
        }
        builder.AppendLine("|");

        // values        
        foreach (var item in _items)
        {
            builder.AppendLine(item.ToString());
        }
        
        return builder.ToString();
    }

    private sealed class TruthTableItem
    {
        public TruthTableItem(IEnumerable<bool> inputs, IEnumerable<bool> outputs)
        {
            Inputs = new List<bool>(inputs);
            Outputs = new List<bool>(outputs);
        }

        public List<bool> Inputs { get; }
        public List<bool> Outputs { get; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var input in Inputs)
            {
                builder.Append("| ")
                    .Append(input ? "1" : "0")
                    .Append(" ");
            }
            builder.Append("|");

            foreach (var output in Outputs)
            {
                builder.Append("| ")
                    .Append(output ? "1" : "0")
                    .Append(" ");
            }
            builder.Append("|");

            return builder.ToString();
        }
    }
}
