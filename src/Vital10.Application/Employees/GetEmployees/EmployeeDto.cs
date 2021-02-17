using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital10.Domain.Employees;

namespace Vital10.Application.Employees.GetEmployees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Employee Partner { get; set; }
    }
}
