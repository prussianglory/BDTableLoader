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
using Npgsql;
using NpgsqlTypes;
using OfficeOpenXml;

namespace MultiTableLoader
{

    public partial class databaseForm : Form
    {
        public DataTableCollection table = null;
        public string[,] stringTable;
        public string[] columns; 
        private string connectionString = "Host=localhost;Username=postgres;Password=Dung30nMa5t3r;Database=dstore;";
        public databaseForm()
        {
            InitializeComponent();
        }


        private void LoadtoDBClick(object sender, EventArgs e)
        {
            try
            {
                if (table != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        // Открываем подключение
                        connection.Open();

                        // Имя таблицы в базе данных (предполагается, что оно совпадает с именем листа в Excel)
                        string tableName = Convert.ToString(LoadedTableComboBox.SelectedItem);

                        // Создание таблицы в БД (если ее нет)
                        CreateTableIfNotExists(connection, tableName, columns);

                        // Вставка данных в таблицу
                        InsertDataIntoTable(connection, tableName, table[tableName]);

                        DisplayDataInDataGridView(tableName);
                    }
                }
                else
                {
                    throw new Exception("Таблица не загружена");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Метод для определения типа данных столбца на основе его значения
        static Type DetermineDataType(string value)
        {
            if (int.TryParse(value, out _))
            {
                return typeof(int);
            }
            else if (double.TryParse(value, out _))
            {
                return typeof(double);
            }
            else if (DateTime.TryParse(value, out _))
            {
                return typeof(DateTime);
            }
            else
            {
                return typeof(string);
            }
        }
        // Метод для получения соответствующего типа данных PostgreSQL
        static string GetPostgresType(Type dataType)
        {
            // Маппинг типов данных C# на типы данных PostgreSQL (можно расширить по необходимости)
            if (dataType == typeof(int))
            {
                return "INT";
            }
            else if (dataType == typeof(double))
            {
                return "DOUBLE PRECISION";
            }
            else if (dataType == typeof(DateTime))
            {
                return "TIMESTAMP";
            }
            else
            {
                return "VARCHAR(255)"; // По умолчанию используем VARCHAR(255) для строковых данных
            }
        }
        // Метод для создания таблицы в БД, если ее не существует
        void CreateTableIfNotExists(NpgsqlConnection connection, string tableName, string[] columns)
        {
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;

                // Генерация строки CREATE TABLE
                string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (";

                foreach (string column in columns)
                {
                    //Type dataType = DetermineDataType(column.ColumnName);
                    //string postgresType = GetPostgresType(dataType);
                    //createTableQuery += $"{column.ColumnName} {postgresType}, ";
                    createTableQuery += $"{column} VARCHAR(255), ";
                    bottomConsole.Text += $"{column} VARCHAR(255), ";
                }

                createTableQuery = createTableQuery.TrimEnd(',', ' ') + ");";
                command.CommandText = createTableQuery;
                command.ExecuteNonQuery();
            }
        }

        // Метод для вставки данных в таблицу в БД строка за строкой
         void InsertDataIntoTable(NpgsqlConnection connection, string tableName, DataTable dataTable)
        {
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;

                // Генерация строки INSERT
                string insertQuery = $"INSERT INTO {tableName} VALUES (";
                bottomConsole.Text += insertQuery+"\n";
                
                foreach (DataColumn column in dataTable.Columns)
                {
                    string cur_value = $"@{column.ColumnName}, ";
                    insertQuery += cur_value;
                    bottomConsole.Text += insertQuery+"\n";
                    NpgsqlDbType dbType = GetNpgsqlDbType(column.DataType);
                    command.Parameters.Add(new NpgsqlParameter($"@{column.ColumnName}", dbType));
                }

                insertQuery = insertQuery.TrimEnd(',', ' ') + ");";
                bottomConsole.Text += ");";
                command.CommandText = insertQuery;
                
                // Вставка данных строка за строкой
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        command.Parameters[i].Value = row[i];
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
        // Функция для преобразования типа .NET в NpgsqlDbType
        private NpgsqlDbType GetNpgsqlDbType(Type type)
        {
            if (type == typeof(int))
                return NpgsqlDbType.Integer;
            else if (type == typeof(double))
                return NpgsqlDbType.Double;
            // Добавьте другие типы данных, если необходимо

            // По умолчанию используйте текст, если тип неизвестен
            return NpgsqlDbType.Text;
        }
        public void DisplayDataInDataGridView(string tableName)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = $"SELECT * FROM {tableName}";
                    using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
