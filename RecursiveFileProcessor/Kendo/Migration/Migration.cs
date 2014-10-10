using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecursiveFileProcessor.Kendo.CodeFrame;

namespace RecursiveFileProcessor.Kendo.Migration
{
    public class Migration
    {
        public List<IMigrationRule> MigrationRules { get; private set; }
        public Logger MigrationLog { get; private set; }

        public Migration()
        {
            MigrationRules = new List<IMigrationRule>();
            MigrationLog = new Logger();
        }

        public void ApplyMigration(Statement st)
        {
            try
            {
                foreach (var mr in MigrationRules)
                {
                    mr.ApplyTo(st, MigrationLog);
                }
            }
            catch (MigrationException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
