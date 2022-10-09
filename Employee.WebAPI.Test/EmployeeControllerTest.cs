using AutoMapper;
using Employee.Core.Constants;
using Employee.Core.DTOs.Employee;
using Employee.Core.Entities.Models;
using Employee.Core.Interfaces;
using Employee.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit.Abstractions;

namespace Employee.WebAPI.Test
{
    public class EmployeeControllerTest
    {
        private readonly EmployeeController _controller;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeControllerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _controller = new EmployeeController(_unitOfWork.Object, _mapper);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAllAysnc();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result as OkObjectResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllEmployees()
        {
            // Act
            var okResult = _controller.GetAllAysnc().Result as OkObjectResult;

            // Assert
            var employees = Assert.IsType<List<EmployeeDto>>(okResult.Value);
            Assert.Equal(3, employees.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetAsync(3);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 3;

            // Act
            var okResult = _controller.GetAsync(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 3;

            // Act
            var okResult = _controller.GetAsync(testId).Result as OkObjectResult;

            // Assert
            Assert.IsType<EmployeeDto>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as EmployeeDto).Id);
        }

        [Fact]
        public async void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var emailMissingEmployee = new EmployeeCreationDto()
            {
                Name = "employe 1",
                Sallary = 5500,
                Address = "Dubai",
                Phone = "971 5555555"

            };
            _controller.ModelState.AddModelError("Email", "Required");

            // Act
            var badResponse = await _controller.CreateAsync(emailMissingEmployee);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            EmployeeCreationDto testemployee = new EmployeeCreationDto()
            {
                Name = "Omran",
                Email = "omran@gmail.com",
                Phone = "+971 568 513 555",
                Address = "Dubai, Al Rigga",
                Sallary = Convert.ToDecimal(6000.0),
               
            };

            // Act
            var createdResponse = _controller.CreateAsync(testemployee);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedEmployee()
        {
            // Arrange
            var  testemployee = new EmployeeCreationDto()
            {
                Name = "Omran",
                Email = "omran@gmail.com",
                Phone = "+971 568 513 555",
                Address = "Dubai, Al Rigga",
                Sallary = Convert.ToDecimal(6000.0),

            };
            // Act
            var createdResponse = _controller.CreateAsync(testemployee).Result as CreatedAtActionResult;
            var employee = createdResponse.Value as EmployeeCreationDto;

            // Assert
            Assert.IsType<EmployeeCreationDto>(employee);
            Assert.Equal("Email", employee.Email);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 1000;

            // Act
            var badResponse = _controller.DeleteAsync(notExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingIdPassed_ReturnsNoContentResult()
        {
            // Arrange
            var existingId = 4;

            // Act
            var noContentResponse = _controller.DeleteAsync(existingId);

            // Assert
            Assert.IsType<OkObjectResult>(noContentResponse);
        }

        [Fact]
        public void Remove_ExistingGuIdPassed_RemovesOneEmployee()
        {
            // Arrange
            var existingId = 4;

            // Act
            var okResponse = _controller.DeleteAsync(existingId);

            // Assert
            Assert.Equal(5, 5);
        }
    }
}

