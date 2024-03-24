# Cupl Language

- Parses CUPL language
- Creates an Abstract Syntax Tree (AST)
- Optimizes boolean expressions
- Reads in device description
- Lays out fuse map
- generates a (JEDEC) .jed file

---

## CUPL

WinCUPL documentation was used as a language guide for the CUPL language.
The language is case insensitive.

The following syntax is supported:

| Area | Keyword | Variations | Description |
| -- | -- | -- | -- |
| Header | assembly | | Free text. |
| | company | | Free text. |
| | designer | | Free text. |
| | device | | Either `G16V8` or `G22V10`. |
| | format | | `J` for (JEDEC) .jed (default). |
| | date | | Free text. |
| | location | | Free text. |
| | name | | Free text. |
| | partno | | Free text. |
| | revision | | Free text. |
| Pin definition | pin | `pin 1 = x` | Single pin input or output definition. |
| | | `pin [1, 2] = [x, y]` | Pin list input or output definition. |
| | | `pin [1..2] = [x0..1]` | Pin range input or output definition. |
| Equations | var = \<eq> | `x = !y` | Equation result assignment to temporary variable or output pin. |
| | Append | `Append var = <eq1>` | Combines all equations for the same symbol name (var) using the Or operator. |
| | | `Append var = <eq2>` | Results in `var = <eq1> # <eq2>` |
| | \<eq> | x & y | Logic And operator. |
| | | x # y | Logic Or operator. |
| | | x $ y | Logic Xor operator. |
| | | !x | Logic Not operator. |
| Extensions | Q1.\<ext> = \<eq> | | Extensions to support flipflop outputs. |
| | | `Q1.d` | Flipflop data input. |
| | | `Q1.ar` | Flipflop asynchronous reset input. |
| | | `Q1.oe` | Flipflop output enable input. |
| | | `Q1.?` | Flipflop ?? input. |
| Literals | \<prefix>10 | `'b'1` | Base prefixes for literal values (binary). |
| | | `'o'8` | Octal. |
| | | `'d'42` | Decimal. No prefix is also decimal. |
| | | `'h'FF` | Hexadecimal. |

---

## Abstract Syntax Tree

The parsing of the CUPL language is done with Antr4. The Antlr generated parser objects are simplified into an object model called the abstract syntax tree (AST) - not sure if it's still a syntax tree, though.

### `AstDocument`

The root object returned from parsing in a CUPL file. It is a container for header and pin information and the logic equations.

### `AstHeader`

Contains fields for all posible header info.

### `AstPin`

For each pin defined in the CUPL file, an AstPin instance is created. List (`pin [1, 2] = ...`) and Range (`pin [1..2] = ...`) syntax is resolved into multiple `AstPin` instances.

### `AstEquation`

For each logic equation in the CUPL file, a `AstEquation` instance is created.
An equation contains the symbol name and the expression.

### `AstExpression`

A hierarchy of `AstExpression` instances represent a single logic equation in the CUPL file.
Symbol names and literal values are also contained in an `AstExpression` object.

---

## Optimizing Boolean Expressions

TODO

---

## Device Description

Ideally a file would describe each device. In that way it would be possible to introduce new devices fairly easily.

- input pins
- output pins
- device modes (combinatorial, registered or complex)
- number of product terms for each output pin (per mode)
- fuse numbers (matrix)

TBH I have no idea if this is gonna work. Probably first hard code it for the 2 devices and see what pattern emerges.

---

## Layout of Fuse Map

This is the meat of the matter.

Usage of extensions (`.d`, `.ar`) denote that the output module is in registered mode.
Do not assume all devices allow individual selection of output module modes. Some settings are device global.

Detect presence of a Clock input (`.clk`). Some modes make use of the clock input (pin 1) implicitly (for all FlipFlops). Not all modes support defining a custom clock equation.

input -> AND-array -> ORed to output + optional FF / invertor

TODO

---

## Output `.jed` file

When an internal representation for the device has been finalized, it should nor be hard to output a valid `.jed` file.

TODO

---

- [ ] Syntax: Pin declarations can combine different list and range notations.
- [ ] Precedence `()` on expressions.
- [ ] 
