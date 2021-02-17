using System.Threading.Tasks;

namespace Vital10.Domain.Employees
{
    public interface IEmployeeRepository 
    {
        Task<int> AddEmployeeAsync(Employee employee);
        Task<int> RemoveEmployeeByIdAsync(int id);

        Task<int> UpdateEmployeeAsync(Employee employee);
    }
}
