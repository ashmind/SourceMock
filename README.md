# Overview

<a href="https://www.nuget.org/packages/SourceMock/">![](https://img.shields.io/nuget/v/SourceMock?style=for-the-badge)</a>

SourceMock is a C# mocking framework based on source generators.  

This allows for a nicer interface than reflection based frameworks, for example:
```csharp
mock.Setup.Parse().Returns(3);
```
instead of
```csharp
mock.Setup(x => x.Parse(It.IsAny<string>())).Return(3)
```

# Cookbook

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

## Verify calls

```csharp
[assembly: GenerateMocksForAssemblyOf(typeof(IParser))]

using Parsing.Mocks;

var parser = new ParserMock();

parser.Parse("1");
parser.Parse("2");

Assert.Equal(new[] { "1", "2" }, parser.Calls.Parse());
```

## Set up a callback

```csharp
[assembly: GenerateMocksForAssemblyOf(typeof(IParser))]

using Parsing.Mocks;

var parser = new ParserMock();

parser.Setup.Parse().Runs(s => int.Parse(s));

Assert.Equal(1, parser.Parse("1"));
```

# Limitations

## By Design

### Strict Mocks

SourceMock does not provide strict mocks — this is by design.  
I believe that verifying setups blurs the line between Arrange and Assert and decreases test readability. 

Instead, assert `.Calls` at the end of the test to confirm the expected calls.

## Not Yet

These are not _intentionally_ excluded, just not yet supported:
1. Class mocks: custom constructors, calling base methods, mocking protected members
2. Custom default values
3. Custom parameter matchers
4. Setting up output values for ref and out parameters
5. Chained setups
6. Anything more advanced than the above

# Kudos

This framework borrows a lot of its design from [Moq](https://github.com/moq), which is one of the best .NET libraries overall.
