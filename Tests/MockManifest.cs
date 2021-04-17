using SourceMock;
using SourceMock.Tests.Interfaces;

[assembly: GenerateMocksForAssemblyOf(typeof(IMockable), ExcludeRegex = "ExcludedInterface")]
[assembly: GenerateMocksForTypes(typeof(AbstractClass))]