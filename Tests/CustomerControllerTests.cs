using App.DataAccess.Entities;
using App.DataAccess.Repositories;
using App.Web;
using App.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class CustomerControllerTests
    {
        [Fact]
        public async void CustomerControllerDetails()
        {
            // Arrange
            var Crepo = new Mock<ICustomerRepo>();
            var Logger = new Mock<ILogger<CustomersController>>();
            Crepo.Setup(repo => repo.GetById(1))
            .Returns(new Customer()
            {
                FirstName = "Bob",
                LastName = "Jones",
                DefaultLocationId = 2,
                DefaultLocation = new Location()
                {
                    Name="test",
                    Id=2
                }
            });
            var controller = new CustomersController(Crepo.Object, Logger.Object);
            var result = (ViewResult)await controller.Details(1);
            Assert.IsType<CustomerVM>(result.ViewData.Model);
        }
        [Fact]
        public async void CustomerControllerSearch()
        {
            // Arrange
            var Crepo = new Mock<ICustomerRepo>();
            var Logger = new Mock<ILogger<CustomersController>>();
            Crepo.Setup(repo => repo.Search(""))
            .Returns(new List<Customer>(){
                new Customer()
                {
                    FirstName = "Bob",
                    LastName = "Jones",
                    DefaultLocationId = 2
                }});
            var controller = new CustomersController(Crepo.Object, Logger.Object);
            var result = (ViewResult)await controller.Search("");
            Assert.IsType<List<CustomerVM>>(result.ViewData.Model);
        }
        [Fact]
        public async void CustomerControllerCreate()
        {
            // Arrange
            var Crepo = new Mock<ICustomerRepo>();
            var Logger = new Mock<ILogger<CustomersController>>();
            var c = new CustomerVM() { 
            FirstName="Test",
            LastName="test",
            LocationId=2
            };
            Customer C = new Customer() {
                FirstName = "Test",
                LastName = "test",
                DefaultLocationId = 2,
                DefaultLocation = new Location()
                {
                    Name = "test",
                    Id = 2
                }
            };
            Crepo.Setup(repo => repo.Add(It.IsAny<Customer>()))
            .Returns(
                new Customer()
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Jones",
                    DefaultLocationId = 2,
                    Order = new List<Order>(),
                    DefaultLocation = new Location()
                    {
                        Name = "test",
                        Id = 2
                    }
                }
                ) ;
            var controller = new CustomersController(Crepo.Object, Logger.Object);
            var result = (ViewResult)await controller.Create(c);
            Assert.IsType<CustomerVM>(result.ViewData.Model);
        }
    }
}
