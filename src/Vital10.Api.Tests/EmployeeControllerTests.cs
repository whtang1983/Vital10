using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Vital10.Api.Controllers;
using Vital10.Application.Employees.GetEmployees;
using Xunit;

namespace Vital10.Api.Tests
{
    public class EmployeeControllerTests
    {
        private EmployeeController _sut;
        private Mock<IMediator> _mediator;

        public EmployeeControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task Should_Return_EmployeeAsync()
        {
            //Arrange
            int employeeId = 1;
            _mediator
                .Setup(x =>
                        x.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<System.Threading.CancellationToken>()))
                .ReturnsAsync(new EmployeeDto {  Id= employeeId, Name="Vital10Employee", Partner = null }); 

            _sut = new EmployeeController(_mediator.Object);

            //Act
            var actionResult = await _sut.GetById(1);

            //Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();
           

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var employeeDtoResult = Assert.IsAssignableFrom<EmployeeDto>(okObjectResult.Value);
            Assert.Equal(employeeId, employeeDtoResult.Id);
        }
    }
}
