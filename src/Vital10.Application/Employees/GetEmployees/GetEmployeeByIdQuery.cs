using MediatR;
using System;
using Vital10.Application.Configuration.Queries;

namespace Vital10.Application.Employees.GetEmployees
{
    public class GetEmployeeByIdQuery : IQuery<EmployeeDto>
    {
        public int EmployeeId { get; }
        public GetEmployeeByIdQuery(int id)
        {
            EmployeeId = id;
        }

    }
}
