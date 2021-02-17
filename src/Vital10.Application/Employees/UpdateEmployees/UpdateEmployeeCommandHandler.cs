using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Vital10.Application.Configuration.Commands;
using Vital10.Domain.Employees;

namespace Vital10.Application.Employees.CreateEmployees
{
    public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
  

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            /*
                {
                  "id": 1,
                  "name": "Benjamin",
                  "partner": {
                    "id": 4
                  }
                }
            */

            // TODO: Implement unitofwork and domaineventsdispatcher to encapsulate infrasturcture stuff.
 
            return await _employeeRepository.UpdateEmployeeAsync(new Employee { Id = request.Id, Name = request.Name, Partner = request.Partner });
        }
    }
}
