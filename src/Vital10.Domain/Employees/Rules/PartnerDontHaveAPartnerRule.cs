using Vital10.Domain.SeedWork;

namespace Vital10.Domain.Employees.Rules
{
    public class PartnerDontHaveAPartnerRule : IBusinessRule
    {
        private readonly IEmployeePartnerChecker _employeePartnerChecker;

        private readonly int  _employeePartnerId;

        public PartnerDontHaveAPartnerRule(
            IEmployeePartnerChecker employeePartnerChecker,
            int employeePartnerId)
        {
            _employeePartnerChecker = employeePartnerChecker;
            _employeePartnerId = employeePartnerId;
        }

        public bool IsBroken() => !_employeePartnerChecker.PartnerDontHaveAPartner(_employeePartnerId);

        public string Message => "Partner already have a partner.";
    }
}