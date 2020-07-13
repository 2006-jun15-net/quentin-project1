using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
using App.DataAccess.Repositories;
using App.Web;
using App.Web.Controllers;
using App.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Tests
{
    public class CartControllerTests
    {
        [Fact]
        public void CartControllerTest() { 
        // Arrange
        var Irepo = new Mock<IInventoryRepo>();
        var Crepo = new Mock<ICustomerRepo>();
        var Logger = new Mock<ILogger<CartController>>();
            Irepo.Setup(repo => repo.Find(2,15))
            .Returns(new Inventory(){
            Product=new Product()
            {
                Name="Name",
                Price=15
            },
            Location=new Location()
            {
                Name="Here"
            }
            });
        Crepo.Setup(repo => repo.Search(""))
            .Returns(new List<Customer>() { 
            new Customer()
            {
                Id=12,
                FirstName="Test",
                LastName="Test"
            }
            });

        var controller = new CartController(Irepo.Object, Crepo.Object, Logger.Object);
        var result = (ViewResult) controller.Index("[{ \"ProductId\":\"2\",\"LocationId\":\"15\",\"Qty\":\"1\"}]");
        Assert.IsType<List<CartVM>>(result.ViewData.Model);
        }
    }
}
