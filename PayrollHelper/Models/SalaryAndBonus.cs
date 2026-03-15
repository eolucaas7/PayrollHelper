using System;
using System.Collections.Generic;

namespace PayrollHelper.Models
{
    public partial class SalaryAndBonus
    {
        public SalaryAndBonus()
        {
            Taxations = new HashSet<Taxation>();
        }

        public int Id { get; set; }
        public string PaymentType { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal? DefaultAmount { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Taxation> Taxations { get; set; }
    }
}
