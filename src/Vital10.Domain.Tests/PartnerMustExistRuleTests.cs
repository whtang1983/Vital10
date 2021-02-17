using FluentAssertions;
using Moq;
using System;
using Vital10.Domain.Employees;
using Vital10.Domain.Employees.Rules;
using Xunit;

namespace Vital10.Domain.Tests
{
    public class PartnerMustExistRuleTests
    {
        private Mock<IEmployeePartnerChecker> _employeePartnerCheckerMock;

        public PartnerMustExistRuleTests()
        {
            _employeePartnerCheckerMock = new Mock<IEmployeePartnerChecker>();
        }


        [Fact]
        public void PartnerExist()
        {
            // arrange
            _employeePartnerCheckerMock.Setup(x => x.IsExistingEmployeePartner(It.IsAny<int>())).Returns(true);
            var employeePartnerExistRule = new PartnerMustExistRule(_employeePartnerCheckerMock.Object, 1);

            // act
            var result = employeePartnerExistRule.IsBroken();

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void PartnerDoesntExist()
        {
            // arrange
            _employeePartnerCheckerMock.Setup(x => x.IsExistingEmployeePartner(It.IsAny<int>())).Returns(false);
            var employeePartnerExistRule = new PartnerMustExistRule(_employeePartnerCheckerMock.Object, 1);

            // act
            var result = employeePartnerExistRule.IsBroken();

            // assert
            result.Should().BeTrue();
        }
    }
}
