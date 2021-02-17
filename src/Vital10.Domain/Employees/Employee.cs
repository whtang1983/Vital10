using Vital10.Domain.Employees.Rules;
using Vital10.Domain.SeedWork;

namespace Vital10.Domain.Employees
{
    public class Employee : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Employee Partner { get; set; } = null;

        public static Employee CreateEmployee(string name, Employee partner, IEmployeePartnerChecker employeePartnerChecker)
        {
            var employee = new Employee() { Name = name};

            if(partner != null)
            {
                CheckRule(new PartnerMustExistRule(employeePartnerChecker, partner.Id));
                CheckRule(new PartnerDontHaveAPartnerRule(employeePartnerChecker, partner.Id));
                employee.Partner = partner; // TODO: find a better way to get the existing data for partner
            }

            return employee;
        }

        public void AddPartner(Employee partner)
        {
            // todo: add PartnerAddedEvent domain event that can be processed by a UnitOfWork --> DomainEventsDispatcher
        }

        public void ChangePartner(Employee partner)
        {
            // todo: add PartnerChangedEvent domain event that can be processed by a UnitOfWork --> DomainEventsDispatcher
        }

        public void DeletePartner()
        {
            // todo: add PartnerDeletedEvent domain event that can be processed by a UnitOfWork --> DomainEventsDispatcher
        }
    }
}
