using Vital10.Domain.SeedWork;

namespace Vital10.Domain.Employees.Rules
{
    public class PartnerMustExistRule : IBusinessRule
    {
        private readonly IEmployeePartnerChecker _employeePartnerChecker;

        private readonly int  _employeePartnerId;

        public PartnerMustExistRule(
            IEmployeePartnerChecker employeePartnerChecker,
            int employeePartnerId)
        {
            _employeePartnerChecker = employeePartnerChecker;
            _employeePartnerId = employeePartnerId;
        }

        public bool IsBroken() => !_employeePartnerChecker.IsExistingEmployeePartner(_employeePartnerId);

        public string Message => "Partner doesnt exist.";
    }
}