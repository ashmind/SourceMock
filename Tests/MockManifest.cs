using System.Net.WebSockets;
using Azure.Storage.Blobs;
using SourceMock;
using SourceMock.Tests.Interfaces;

[assembly: GenerateMocksForAssemblyOf(typeof(IMockable), ExcludeRegex = "ExcludedInterface")]
[assembly: GenerateMocksForTypes(
    typeof(AbstractClass),
    typeof(Disposable),
    typeof(WebSocket),
    typeof(BlobContainerClient)
)]