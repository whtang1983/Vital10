using Autofac;
using Vital10.Application.Employees.DomainServices;
using Vital10.Domain.Employees;

namespace Vital10.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeePartnerChecker>()
                .As<IEmployeePartnerChecker>()
                .InstancePerLifetimeScope();
        }
    }
}