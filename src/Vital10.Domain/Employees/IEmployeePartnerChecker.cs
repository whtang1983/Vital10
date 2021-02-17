namespace Vital10.Domain.Employees
{
    public interface IEmployeePartnerChecker
    {
        bool IsExistingEmployeePartner(int employeePartnerId);

        bool PartnerDontHaveAPartner(int employeePartnerId);
    }
}