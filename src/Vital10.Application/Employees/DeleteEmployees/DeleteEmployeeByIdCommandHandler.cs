using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Vital10.Application.Configuration.Commands;
using Vital10.Domain.Employees;

namespace Vital10.Application.Employees.DeleteEmployees
{
    public class DeleteEmployeeByIdCommandHandler : ICommandHandler<DeleteEmployeeByIdCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeByIdCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<int> Handle(DeleteEmployeeByIdCommand command, CancellationToken cancellationToken)
        {
            return await _employeeRepository.RemoveEmployeeByIdAsync(command.Id);
        }
    }
}
