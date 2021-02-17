using System;
using System.Collections.Generic;

#nullable disable

namespace Vital10.Infrastructure.Database
{
    public partial class Partner
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? PartnerEmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee PartnerEmployee { get; set; }
    }
}
