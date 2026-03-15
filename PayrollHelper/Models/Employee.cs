using System;
using System.Collections.Generic;

namespace PayrollHelper.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Payments = new HashSet<Payment>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public int PostNumber { get; set; }
        public bool? Insurance { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual Position PostNumberNavigation { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
