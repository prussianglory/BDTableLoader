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
        Dictionary<string, string> columnTypes = new Dictionary<string, string>();
        Dictionary<string, NpgsqlDbType> TypesDict = new Dictionary<string, NpgsqlDbType>()
        {
            {"INTEGER", NpgsqlDbType.Integer},
            {"BIGINT", NpgsqlDbType.Bigint},
            {"TIMESTAMP", NpgsqlDbType.Timestamp }
        };
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
                        CreateTableIfNotExists(connection, table[tableName], columns);

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
        
        // Функция для преобразования типа .NET в тип PostgreSQL
        private string GetPostgreSQLDataTypeForColumn(DataTable dataTable, string columnName)
        {
            // Проверяем содержимое всех ячеек в столбце
            foreach (DataRow row in dataTable.Rows)
            {
                if (int.TryParse(row[columnName].ToString(), out _))
                {
                    return "INTEGER";
                }
                if (DateTime.TryParse(row[columnName].ToString(), out _))
                {
                    return "TIMESTAMP";
                }
                else if (long.TryParse(row[columnName].ToString(), out _))
                {
                    return "BIGINT";
                }
                
            }

            // Если не удалось определить тип, используем VARCHAR(255) по умолчанию
            return "VARCHAR(255)";
        }
        // Метод для создания таблицы в БД, если ее не существует
        void CreateTableIfNotExists(NpgsqlConnection connection, DataTable table, string[] columns)
        {

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;

                string createTableQuery = $"CREATE TABLE IF NOT EXISTS {table.TableName} (";

                foreach (DataColumn column in table.Columns)
                {
                    string columnName = column.ColumnName;
                    string columnType = GetPostgreSQLDataTypeForColumn(table, columnName);
                    //createTableQuery += $"{columnName} {columnType}, ";
                    createTableQuery += $"{columnName} VARCHAR(255), ";

                    // Сохраняем информацию о типах столбцов
                    columnTypes[columnName] = columnType;
                }

                createTableQuery = createTableQuery.TrimEnd(',', ' ') + ");";
                command.CommandText = createTableQuery;
                command.ExecuteNonQuery();
            }
            /*
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;

                // Генерация строки CREATE TABLE
                string createTableQuery = $"CREATE TABLE IF NOT EXISTS {table.TableName} (";

                foreach (string column in columns)
                {
                    createTableQuery += $"{column} VARCHAR(255), ";
                    bottomConsole.Text += $"{column} VARCHAR(255), ";
                }

                createTableQuery = createTableQuery.TrimEnd(',', ' ') + ");";
                command.CommandText = createTableQuery;
                command.ExecuteNonQuery();
            }
            */
        }

        // Метод для вставки данных в таблицу в БД строка за строкой
         void InsertDataIntoTable(NpgsqlConnection connection, string tableName, DataTable dataTable)
        {
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;

                // Генерация строки INSERT
                string insertQuery = $"INSERT INTO {tableName} VALUES (";
                bottomConsole.Text += insertQuery + "\n";

                foreach (DataColumn column in dataTable.Columns)
                {
                    string cur_value = $"@{column.ColumnName}, ";
                    insertQuery += cur_value;
                    bottomConsole.Text += insertQuery + "\n";
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
                foreach (DataColumn column in dataTable.Columns)
                {
                    insertQuery = $"ALTER TABLE {tableName} ";
                    insertQuery += $"ALTER COLUMN {column.ColumnName} TYPE {columnTypes[column.ColumnName]} USING {column.ColumnName}::{columnTypes[column.ColumnName]};";
                    command.CommandText = insertQuery;
                    command.ExecuteNonQuery();
                }
            }
        }



        // Функция для преобразования типа .NET в DbType
        private DbType GetDbType(Type type)
        {
            if (type == typeof(int))
                return DbType.Int32;
            else if (type == typeof(long))
                return DbType.Int64;
            else if (type == typeof(DateTime))
                return DbType.DateTime;
            // Добавьте другие типы данных, если необходимо

            // По умолчанию используйте текст, если тип неизвестен
            return DbType.String;
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
