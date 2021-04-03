using System;
using SourceMock;

public static class SourceMockExtensions {
    public static void Throws<TException>(this IMockMethodSetup setup)
        where TException : Exception, new()
    {
        setup.Throws(new TException());
    }
}