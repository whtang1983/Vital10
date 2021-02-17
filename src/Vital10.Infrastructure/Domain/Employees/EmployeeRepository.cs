using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vital10.Application.Employees.CreateEmployees;
using Vital10.Domain.Employees;
using Vital10.Infrastructure.Database;
using EmployeeDB = Vital10.Infrastructure.Database.Employee;
using PartnerDB = Vital10.Infrastructure.Database.Partner;

namespace Vital10.Infrastructure.Domain.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeesContext _context;

        public EmployeeRepository(EmployeesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        private async Task<EmployeeDB> GetEmployeeAsync(int employeeId)
        {
            return await _context.Employees.Where(a => a.Id == employeeId).FirstOrDefaultAsync();
        }

        public async Task<int> AddEmployeeAsync(Vital10.Domain.Employees.Employee employee)
        {
            var employeeEntity = new EmployeeDB { Name = employee.Name };

            // create employee
            _context.Employees.Add(employeeEntity);
            await _context.SaveChangesAsync();

            if (employee.Partner != null)
            {

                var existingPartnerEmployeeEntity = await GetEmployeeAsync(employee.Partner.Id);
                if (existingPartnerEmployeeEntity != null)
                {
                    // create partners
                    var employeePartnerEntity = new PartnerDB { EmployeeId = employeeEntity.Id, PartnerEmployeeId = existingPartnerEmployeeEntity.Id };
                    var partnerEmployeeEntity = new PartnerDB { EmployeeId = existingPartnerEmployeeEntity.Id, PartnerEmployeeId = employeeEntity.Id };
                    _context.Partners.AddRange(new PartnerDB[] { employeePartnerEntity, partnerEmployeeEntity });
                    await _context.SaveChangesAsync();
                }

                // what do you do when no employees are founded based on the given Partner.Id?  throw exception?

            }

            return employeeEntity.Id;
        } 

        public async Task<int> RemoveEmployeeByIdAsync(int employeeId)
        {
            var employeeEntity = await GetEmployeeAsync(employeeId);

            if (employeeEntity == null)
                return default;

            // delete partners
            var partnerEntities = await _context.Partners.Where(x => x.EmployeeId == employeeId || x.PartnerEmployeeId == employeeId).ToArrayAsync();
            _context.Partners.RemoveRange(partnerEntities);

            // remove employee
            _context.Employees.Remove(employeeEntity);
            await _context.SaveChangesAsync();

            return employeeEntity.Id;
        }

        
        public async Task<int> UpdateEmployeeAsync(Vital10.Domain.Employees.Employee employee)
        {
            var employeeEntity = await GetEmployeeAsync(employee.Id);

            if (employeeEntity == null)
            {
                return default;
            }
            else
            {
                employeeEntity.Name = employee.Name;

                // delete partners
                // todo: how to deal with changing to partners who already have a partner?
                var partnerEntities = await _context.Partners.Where(x => x.EmployeeId == employee.Id || x.PartnerEmployeeId == employee.Id).ToArrayAsync();
                _context.Partners.RemoveRange(partnerEntities);
                
                if (employee.Partner != null)
                {
                    // create partners
                    var employeePartnerEntity = new PartnerDB { EmployeeId = employeeEntity.Id, PartnerEmployeeId = employee.Partner.Id };
                    var partnerEmployeeEntity = new PartnerDB { EmployeeId = employee.Partner.Id, PartnerEmployeeId = employeeEntity.Id };
                    _context.Partners.AddRange(new PartnerDB[] { employeePartnerEntity, partnerEmployeeEntity });
                }
            
                await _context.SaveChangesAsync();

                return employeeEntity.Id;
            }
            
        }

      


    }
}
