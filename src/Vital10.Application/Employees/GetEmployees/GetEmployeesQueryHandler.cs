using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vital10.Application.Configuration.Data;
using Vital10.Application.Configuration.Queries;
using Vital10.Domain.Employees;

namespace Vital10.Application.Employees.GetEmployees
{
    public class GetEmployeesQueryHandler : IQueryHandler<GetEmployeesQuery, IList<EmployeeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetEmployeesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IList<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            // This method should be improved. we are calling multiple queries to get the partner information.
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var query = @"SELECT E.Id, E.Name
                          FROM [dbo].[Employee] E;";
            var employees = await connection.QueryAsync<Employee>(query);


            var partnerQuery = @" SELECT E.Id, E.Name
                          FROM [dbo].[Employee] E
                               LEFT OUTER JOIN [dbo].[Partner] P ON E.Id = P.EmployeeId
                          WHERE P.PartnerEmployeeId = @Id;";

            var result = new List<EmployeeDto>();
            foreach(Employee employee in employees)
            {
                // we are assuming an employee can only have one partner at a time.
                var partner = await connection.QueryFirstOrDefaultAsync<Employee>(partnerQuery, new
                {
                    employee.Id
                });

                var employeeDto = new EmployeeDto { Id = employee.Id, Name = employee.Name };

                if (partner != null)
                {
                    partner.Partner = employee;
                    employeeDto.Partner = partner;
                }

                result.Add(employeeDto);
            }

            return result;
            
        }
    }
}
