using System;
using System.Collections.Generic;

namespace PayrollHelper.Models
{
    public partial class Taxation
    {
        public Taxation()
        {
            SalaryAndBonuses = new HashSet<SalaryAndBonus>();
        }

        public int Id { get; set; }
        public string TaxType { get; set; } = null!;
        public decimal TaxRate { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<SalaryAndBonus> SalaryAndBonuses { get; set; }
    }
}
