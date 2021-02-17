using Dapper;
using Vital10.Application.Configuration.Data;
using Vital10.Domain.Employees;

namespace Vital10.Application.Employees.DomainServices
{
    public class EmployeePartnerChecker : IEmployeePartnerChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public EmployeePartnerChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsExistingEmployeePartner(int employeePartnerId)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            var query = @"SELECT TOP 1 E.Id
                          FROM [dbo].[Employee] E
                          WHERE E.Id = @EmployeeId;";

            var employeeId = connection.QuerySingleOrDefault<int?>(query,
                            new
                            {
                                EmployeeId = employeePartnerId
                            });

            return employeeId.HasValue;
        }

        public bool PartnerDontHaveAPartner(int employeePartnerId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var query = @"SELECT TOP 1 P.Id
                          FROM [dbo].[Partner] P
                          WHERE P.EmployeeId = @PartnerEmployeeId;";

            var employeeId = connection.QuerySingleOrDefault<int?>(query,
                            new
                            {
                                PartnerEmployeeId = employeePartnerId
                            });

            return !employeeId.HasValue;
        }


  

    }
}