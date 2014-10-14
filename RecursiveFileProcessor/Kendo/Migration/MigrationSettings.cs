using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecursiveFileProcessor.Kendo.MigrateTelerikGridToKendo;

namespace RecursiveFileProcessor.Kendo.Migration
{
    public partial class MigrationSettings : Form
    {
        private CheckBox chkClientEventsRemake;
        private CheckBox chkCommandRenamingAndButtonTypeRemoval;
        private CheckBox chkCRUDRename;
        private CheckBox chkDataBindingMovement;
        private CheckBox chkDataKeysMovement;
        private CheckBox chkDefaultDataItemMovement;
        private CheckBox chkGridEditModeChange;
        private CheckBox chkInsertRowPositionChange;
        private CheckBox chkOperationModeRemake;
        private CheckBox chkPagerSettingsMovement;
        private CheckBox chkRemoveFormHtmlAttributes;

        public Migration GetFullMigration()
        {
            var migration = new Migration();
            migration.MigrationRules.Add(new DataBindingMovement());
            migration.MigrationRules.Add(new DataKeysMovement());
            migration.MigrationRules.Add(new CommandRenamingAndButtonTypeRemoval());
            migration.MigrationRules.Add(new CRUDRename());
            migration.MigrationRules.Add(new ClientEventsRemake());
            migration.MigrationRules.Add(new DefaultDataItemMovement());
            migration.MigrationRules.Add(new GridEditModeChange());
            migration.MigrationRules.Add(new InsertRowPositionChange());
            migration.MigrationRules.Add(new OperationModeRemake());
            migration.MigrationRules.Add(new PagerSettingsMovement());
            migration.MigrationRules.Add(new RemoveFormHtmlAttributes());
            return migration;
        }

        public Migration GetMigration()
        {
            var migration = new Migration();
            if (chkClientEventsRemake.Checked)
            {
                migration.MigrationRules.Add(new ClientEventsRemake());
            }
            if (chkCommandRenamingAndButtonTypeRemoval.Checked)
            {
                migration.MigrationRules.Add(new CommandRenamingAndButtonTypeRemoval());
            }
            if (chkCRUDRename.Checked)
            {
                migration.MigrationRules.Add(new CRUDRename());
            }
            if (chkDataBindingMovement.Checked)
            {
                migration.MigrationRules.Add(new DataBindingMovement());
            }
            if (chkDataKeysMovement.Checked)
            {
                migration.MigrationRules.Add(new DataKeysMovement());
            }
            if (chkDefaultDataItemMovement.Checked)
            {
                migration.MigrationRules.Add(new DefaultDataItemMovement());
            }
            if (chkGridEditModeChange.Checked)
            {
                migration.MigrationRules.Add(new GridEditModeChange());
            }
            if (chkInsertRowPositionChange.Checked)
            {
                migration.MigrationRules.Add(new InsertRowPositionChange());
            }
            if (chkOperationModeRemake.Checked)
            {
                migration.MigrationRules.Add(new OperationModeRemake());
            }
            if (chkPagerSettingsMovement.Checked)
            {
                migration.MigrationRules.Add(new PagerSettingsMovement());
            }
            if (chkRemoveFormHtmlAttributes.Checked)
            {
                migration.MigrationRules.Add(new RemoveFormHtmlAttributes());
            }

            return migration;

        }

        public MigrationSettings()
        {
            InitializeComponent();
            InitializeCheckboxes();
        }

        public void InitializeCheckboxes()
        {
            int y = 10;
            int yShift = 25;
            int x = 10;

            chkClientEventsRemake = new CheckBox { Text = "ClientEvents remake", Location = new Point(x, y), Checked = true };
            chkClientEventsRemake.AutoSize = true;
            y += yShift;
            Controls.Add(chkClientEventsRemake);
            AutoSize = true;
            //MaximumSize = Size;

            chkCommandRenamingAndButtonTypeRemoval = new CheckBox { Text = "Command renaming and ButtonType removal", Location = new Point(x, y), Checked = true };
            chkCommandRenamingAndButtonTypeRemoval.AutoSize = true;
            y += yShift;
            Controls.Add(chkCommandRenamingAndButtonTypeRemoval);

            chkCRUDRename = new CheckBox { Text = "CRUD rename (Required for comment)", Location = new Point(x, y), Checked = true };
            chkCRUDRename.AutoSize = true;
            y += yShift;
            Controls.Add(chkCRUDRename);

            chkDataBindingMovement = new CheckBox { Text = "DataBinding movement", Location = new Point(x, y), Checked = true };
            chkDataBindingMovement.AutoSize = true;
            y += yShift;
            Controls.Add(chkDataBindingMovement);

            chkDataKeysMovement = new CheckBox { Text = "DataKeys movement", Location = new Point(x, y), Checked = true };
            chkDataKeysMovement.AutoSize = true;
            y += yShift;
            Controls.Add(chkDataKeysMovement);

            chkDefaultDataItemMovement = new CheckBox { Text = "DefaultDataItem movement (Required for comment)", Location = new Point(x, y), Checked = true };
            chkDefaultDataItemMovement.AutoSize = true;
            y += yShift;
            Controls.Add(chkDefaultDataItemMovement);

            chkGridEditModeChange = new CheckBox { Text = "GridEditMode change", Location = new Point(x, y), Checked = true };
            chkGridEditModeChange.AutoSize = true;
            y += yShift;
            Controls.Add(chkGridEditModeChange);

            chkInsertRowPositionChange = new CheckBox { Text = "InsertRowPosition change", Location = new Point(x, y), Checked = true };
            chkInsertRowPositionChange.AutoSize = true;
            y += yShift;
            Controls.Add(chkInsertRowPositionChange);

            chkOperationModeRemake = new CheckBox { Text = "OperationMode remake", Location = new Point(x, y), Checked = true };
            chkOperationModeRemake.AutoSize = true;
            y += yShift;
            Controls.Add(chkOperationModeRemake);

            chkPagerSettingsMovement = new CheckBox { Text = "Pager settings movement", Location = new Point(x, y), Checked = true };
            chkPagerSettingsMovement.AutoSize = true;
            y += yShift;
            Controls.Add(chkPagerSettingsMovement);

            chkRemoveFormHtmlAttributes = new CheckBox { Text = "Remove FormHtmlAttributes", Location = new Point(x, y), Checked = true };
            chkRemoveFormHtmlAttributes.AutoSize = true;
            y += yShift;
            Controls.Add(chkRemoveFormHtmlAttributes);

            var okayButton = new Button();
            okayButton.Location = new Point(x, y);
            okayButton.Click += okayButton_click;
            okayButton.Text = "OK";
            okayButton.Size = new Size(70, yShift);
            Controls.Add(okayButton);

            var cancelButton = new Button();
            cancelButton.Location = new Point(x + okayButton.Size.Width, y);
            cancelButton.Click += cancelButton_click;
            cancelButton.Text = "Cancel";
            cancelButton.Size = new Size(70, yShift);
            Controls.Add(cancelButton);


        }

        private void okayButton_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
