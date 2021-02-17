using System;
using System.Collections.Generic;

#nullable disable

namespace Vital10.Infrastructure.Database
{
    public partial class Employee
    {
        public Employee()
        {
            PartnerEmployees = new HashSet<Partner>();
            PartnerPartnerEmployees = new HashSet<Partner>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Partner> PartnerEmployees { get; set; }
        public virtual ICollection<Partner> PartnerPartnerEmployees { get; set; }
    }
}
