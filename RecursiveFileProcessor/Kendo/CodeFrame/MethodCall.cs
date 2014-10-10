﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public class MethodCall : MethodCallBase
    {
        public string MethodName { get; set; }
        public List<IArgumentBase> Arguments { get; private set; }
        public bool IsProperty { get; set; }
        public int Indent { get; set; }


        public MethodCall() : this(string.Empty, false)
        {
        }

        public MethodCall(string methodName) : this(methodName, false)
        {
        }

        public MethodCall(string methodName, bool isProperty)
        {
            Arguments = new List<IArgumentBase>();
            MethodName = methodName;
            IsProperty = isProperty;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(MethodName)) return string.Empty;
            
            if (IsProperty) return MethodName;
            Arguments.ForEach(arg => arg.Indent = Indent + 1);

            var builder = new StringBuilder(MethodName);
            builder.Append("(");
            if (Arguments.Count != 0)
            {
                builder.Append(
                    Arguments.Select(arg => arg.ToString()).Aggregate((working, next) => working + ", " + next));
            }
            builder.Append(")");
            return builder.ToString();
        }
    }
}
