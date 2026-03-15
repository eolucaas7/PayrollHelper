using System;
using System.Collections.Generic;

namespace PayrollHelper.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentType { get; set; } = null!;
        public decimal PaymentAmount { get; set; }
        public DateOnly PaymentDate { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
