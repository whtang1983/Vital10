using Dapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vital10.Application.Configuration.Data;
using Vital10.Application.Configuration.Queries;
using Vital10.Domain.Employees;

namespace Vital10.Application.Employees.GetEmployees
{
    public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetEmployeeByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var query = @"SELECT E.Id, E.Name
                          FROM [dbo].[Employee] E
                          WHERE E.Id = @EmployeeId;

                          SELECT E.Id, E.Name
                          FROM [dbo].[Employee] E
                               LEFT OUTER JOIN [dbo].[Partner] P ON E.Id = P.Id
                          WHERE P.PartnerEmployeeId = @EmployeeId;";

            using (var multi = await connection.QueryMultipleAsync(query, new { request.EmployeeId }))
            {
                // the first query should return 1 employee record
                var employees = multi.Read<Employee>().ToList();

                if(employees.Count == 0)
                {
                    return new EmployeeDto(); 
                }

                // the second query should return the partner (if there is any)
                var partners = multi.Read<Employee>().ToList();

                if (partners.Count == 0)
                {
                    var employee = employees.FirstOrDefault();
                    return new EmployeeDto { Id = employee.Id, Name = employee.Name, Partner = null };
                }

                var universe = employees.SelectMany(employee => partners.Select(partner => (employee, partner)));

                foreach (var (employee, partner) in universe)
                {
                    employee.Partner = partner;
                    partner.Partner = employee;

                    // this method doesnt support multiple partners. immediately return a result when 1 partner is found.
                    return new EmployeeDto { Id = employee.Id, Name = employee.Name, Partner = employee.Partner };
                }
            }

            return new EmployeeDto();
        }
    }
}
