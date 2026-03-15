using Microsoft.EntityFrameworkCore;
using PayrollHelper.Models;
using System.Text;

namespace PayrollHelper
{
    internal static class Program
    {
        public static PayrollDbContext dbContext; // Доступно во всем приложении

        [STAThread]
        static void Main()
        {
            // ОБЯЗАТЕЛЬНО: Регистрируем провайдер кодировок для поддержки кириллицы (WIN1251)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Настройка контекста БД (Используем БАЗОВЫЙ тип payroll_dbContext для опций)
            var optionsBuilder = new DbContextOptionsBuilder<payroll_dbContext>();
            string connectionString = "Host=localhost;Database=payroll_db;Username=payroll_user;Password=123;Client Encoding=UTF8";
            
            optionsBuilder.UseNpgsql(connectionString);
            
            // Инициализация контекста (Теперь типы совпадают)
            dbContext = new PayrollDbContext(optionsBuilder.Options);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
