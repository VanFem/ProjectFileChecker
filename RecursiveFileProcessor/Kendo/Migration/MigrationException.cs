﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFileProcessor.Kendo.Migration
{
    public class MigrationException : Exception
    {
        public MigrationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
