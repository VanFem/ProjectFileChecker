using System.Collections.Generic;

namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public interface IMethodStatement : IStatement
    {
        List<MethodCall> MethodCalls { get; }
    }
}