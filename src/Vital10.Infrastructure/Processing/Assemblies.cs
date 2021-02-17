using System.Reflection;
using Vital10.Application.Employees.CreateEmployees;

namespace Vital10.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(CreateEmployeeCommand).Assembly;
    }
}