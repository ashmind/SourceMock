# Overview

SourceMock is a C# mocking framework based on source generators.  

This potentially allows for a nicer interface than reflection based frameworks, for example:
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
1. Default values (e.g. empty collections instead of null)
2. ref/out parameters and ref returns
3. Generic methods
4. Events
5. Pretty much anything fancy

However if you want to learn source generators, or your use cases are quite straightforward, it might actually work.

# Getting started

## Set up a simple mock

```csharp
interface IParser { int Parse(string value); }

var parser = Mock.Of<IParser>().Get();
parser.Setup.Parse().Returns(1);

Assert.Equal(1, parser.Parse());
```

## Verifying calls

```csharp
interface IParser { int Parse(string value); }

var parser = Mock.Of<IParser>().Get();

parser.Parse("1");
parser.Parse("2");

Assert.Equal(new[] { "1", "2" }, parser.Calls.Parse());
```

# Kudos

This framework borrows a lot of its design from [Moq](https://github.com/moq), which is one of the best .NET libraries overall.