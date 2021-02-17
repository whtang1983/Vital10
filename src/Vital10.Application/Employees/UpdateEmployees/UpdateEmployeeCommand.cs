using Vital10.Application.Configuration.Commands;
using Vital10.Application.Employees.GetEmployees;
using Vital10.Domain.Employees;

namespace Vital10.Application.Employees.CreateEmployees{
    public class UpdateEmployeeCommand : ICommand<int>
    {
        public int Id { get; }
        public string Name { get; }
        public Employee Partner { get; }
    
        public UpdateEmployeeCommand(int id, string name, Employee partner)
        {
            Id = id;
            Name = name;
            Partner = partner;
        }
    }
}
