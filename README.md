# Overview

SourceMock is a C# mocking framework based on source generators.  

This allows for a nicer interface than reflection based frameworks, for example:
```csharp
mock.Setup.Parse().Returns(3);
```
instead of
```csharp
mock.Setup(x => x.Parse(It.IsAny<string>())).Return(3)
```

# Status

The framework is NOT ready.  

Abridged list of things that do not work:
- Generic methods

# Getting started

All examples assume the following interface:
```csharp
namespace Parsing {
    interface IParser { int Parse(string value); }
}
```

## Set up a simple mock

```csharp
[assembly: GenerateMocksForAssemblyOf(typeof(IParser))]

using Parsing.Mocks;

var parser = new ParserMock();

parser.Setup.Parse().Returns(1);

Assert.Equal(1, parser.Parse());
```

## Verifying calls

```csharp
[assembly: GenerateMocksForAssemblyOf(typeof(IParser))]

using Parsing.Mocks;

var parser = new ParserMock();

parser.Parse("1");
parser.Parse("2");

Assert.Equal(new[] { "1", "2" }, parser.Calls.Parse());
```

# Limitations

The following things are not (yet?) supported:
1. Custom parameter matchers
2. Custom mock callbacks
3. Setting up output values for ref and out parameters
4. Anything more advanced than the above

# Kudos

This framework borrows a lot of its design from [Moq](https://github.com/moq), which is one of the best .NET libraries overall.