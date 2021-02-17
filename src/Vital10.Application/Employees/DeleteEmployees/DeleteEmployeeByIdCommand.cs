using System;
using Vital10.Application.Configuration.Commands;
using Vital10.Application.Employees.GetEmployees;

namespace Vital10.Application.Employees.DeleteEmployees{
    public class DeleteEmployeeByIdCommand : ICommand<int>
    {
        public int Id { get; set; }
    }
}
