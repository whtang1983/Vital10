using Vital10.Application.Configuration.Commands;
using Vital10.Application.Employees.GetEmployees;
using Vital10.Domain.Employees;

namespace Vital10.Application.Employees.CreateEmployees{
    public class CreateEmployeeCommand : ICommand<int>
    {
        public string Name { get; }
        public Employee Partner { get; }
    
        public CreateEmployeeCommand(string name, Employee partner)
        {
            Name = name;
            Partner = partner;
        }
    }
}
