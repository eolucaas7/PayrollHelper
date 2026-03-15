using Npgsql;
using System.Data;

namespace PayrollHelper
{
    internal class LoadDB
    {
        // Только строка подключения - статическая, остальное создаем локально
        public static string Connection_String = "Host=localhost;Username=payroll_user;Password=123;Database=payroll_db;Client Encoding=UTF8;";

        // Метод для получения нового соединения с принудительной UTF-8 кодировкой
        public static NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(Connection_String);
            conn.Open();

            // Принудительно установить кодировку клиента
            using (var cmd = new NpgsqlCommand("SET client_encoding TO 'UTF8'", conn))
            {
                cmd.ExecuteNonQuery();
            }

            return conn;
        }

        // Метод для выполнения SQL-запросов, не возвращающих данных
        public void execute(string query)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Ошибка PostgreSQL: {ex.Message}");
                throw; // Пробросить исключение дальше, чтобы форма узнала об ошибке
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                throw;
            }
        }

        // Метод для выполнения SQL-запроса, возвращающего список строк
        public List<string> getRes(string command)
        {
            List<string> res = new List<string>();

            try
            {
                using (var conn = GetConnection())
                using (var cmd = new NpgsqlCommand(command, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(Convert.ToString(reader.GetValue(0)));
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Ошибка PostgreSQL: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                throw;
            }

            return res;
        }

        // Метод для выполнения SQL-запроса и получения данных в формате DataTable
        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var conn = GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Ошибка PostgreSQL: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                throw;
            }

            return dt;
        }
    }
}