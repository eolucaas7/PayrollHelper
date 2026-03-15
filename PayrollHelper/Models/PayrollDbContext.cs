using Microsoft.EntityFrameworkCore;

namespace PayrollHelper.Models
{
    public partial class PayrollDbContext : payroll_dbContext
    {
        public PayrollDbContext(DbContextOptions<payroll_dbContext> options)
            : base(options)
        {
        }
    }
}