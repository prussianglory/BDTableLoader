using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using System.IO;

namespace MultiTableLoader
{
    public partial class MainForm : Form
    {
        ExcelForm eForm = new ExcelForm();
        databaseForm dbForm = new databaseForm();
        
        public MainForm()
        {
            InitializeComponent();
            eForm.TableUpdated += HandleTableUpdated;
            LoadForm(this.eForm);
            
        }
        private void HandleTableUpdated(object sender, DataTableCollection t)
        {
            dbForm.table = t;
            eForm.fileName = Path.GetFileName(eForm.fileName);
            dbForm.LoadedTableLabel.Text = eForm.fileName;
            dbForm.LoadedTableComboBox.Items.Clear();
            foreach (DataTable table in dbForm.table)
            {
                dbForm.LoadedTableComboBox.Items.Add(table.TableName);
            }

            dbForm.LoadedTableComboBox.SelectedIndex = 0;
            dbForm.columns = eForm.GetColumnNames(dbForm.table[dbForm.LoadedTableComboBox.SelectedIndex]);
            dbForm.stringTable = DataTableToArray(dbForm.table[dbForm.LoadedTableComboBox.SelectedIndex]);
            /*
            for (int i = 0; i < dbForm.columns.Length; i++)
            {
                dbForm.bottomConsole.Text += $"{dbForm.columns[i]} ";
            }
            dbForm.bottomConsole.Text += "\n\n";
            for (int i = 0; i < dbForm.stringTable.GetLength(0); i++)
            {
                for (int j = 0; j < dbForm.stringTable.GetLength(1); j++)
                {
                    dbForm.bottomConsole.Text += $"{dbForm.stringTable[i, j]} ";
                }
                dbForm.bottomConsole.Text += "\n";
            }
            */
            //dbForm.Update();

        }
        
        static string[,] DataTableToArray(DataTable dataTable)
        {
            int rows = dataTable.Rows.Count;
            int cols = dataTable.Columns.Count;
            string[,] array = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = dataTable.Rows[i][j].ToString();
                }
            }

            return array;
        }
        public void LoadForm(object Form)
        {
            if (this.mainPanel.Controls.Count > 0)
                this.mainPanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }

        private void clickExcelBtn(object sender, EventArgs e)
        {
            LoadForm(this.eForm);
        }

        private void clickDBBtn(object sender, EventArgs e)
        {
            LoadForm(this.dbForm);
        }

      

    }
}
