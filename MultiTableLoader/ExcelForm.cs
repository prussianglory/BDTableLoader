using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using OfficeOpenXml;

namespace MultiTableLoader
{
    public partial class ExcelForm : Form
    {
        public string fileName = string.Empty;

        public event EventHandler<DataTableCollection> TableUpdated;


        private DataTableCollection tableCollection = null;
        public DataTableCollection TableCollection
        {
            get { return tableCollection; }
            set
            {
                tableCollection = value;
                TableUpdated?.Invoke(this, tableCollection);
            }
        }

        public DataTable table;
        public ExcelForm()
        {
            InitializeComponent();
        }

        private void openExcelFileClick(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                    Text = fileName;
                    OpenExcelFile(fileName);
                }
                else
                {
                    throw new Exception("Файл не выбран");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenExcelFile(string path)
        {
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
            
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

            DataSet db = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            }
            );
            TableCollection = db.Tables;
            LoadedTableComboBox.Items.Clear();
            foreach (DataTable table in TableCollection)
            {
                LoadedTableComboBox.Items.Add(table.TableName);
            }

            LoadedTableComboBox.SelectedIndex = 0;  
            

        }
        public string[] GetColumnNames(DataTable dataTable)
        {
            // Используем LINQ для проекции имен столбцов
            var columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
            return columnNames;
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.table = TableCollection[Convert.ToString(LoadedTableComboBox.SelectedItem)];

            dataGridView1.DataSource = table;
        }
    }
}
