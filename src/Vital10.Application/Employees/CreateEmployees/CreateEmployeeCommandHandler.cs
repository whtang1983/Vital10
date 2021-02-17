using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Vital10.Application.Configuration.Commands;
using Vital10.Domain.Employees;

namespace Vital10.Application.Employees.CreateEmployees
{
    public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IEmployeePartnerChecker _employeePartnerChecker;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEmployeePartnerChecker employeePartnerChecker)
        {
            _employeeRepository = employeeRepository;
            _employeePartnerChecker = employeePartnerChecker;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
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

            var employee = Employee.CreateEmployee(request.Name, request.Partner, _employeePartnerChecker);

            return await _employeeRepository.AddEmployeeAsync(employee);
        }
    }
}
