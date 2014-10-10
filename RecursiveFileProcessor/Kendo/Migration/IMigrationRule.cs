using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursiveFileProcessor.Kendo.CodeFrame;

namespace RecursiveFileProcessor.Kendo.Migration
{
    public interface IMigrationRule
    {
        void ApplyTo(Statement statement, Logger log);
    }
}
