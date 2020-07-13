using App.DataAccess.Entities;
using App.DataAccess.Repositories;
using App.Web;
using App.Web.Controllers;
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
    public class OrdersControllerTests
    {
        [Fact]
        public async void OrderControllerDetails()
        {
            var Lrepo = new Mock<IOrderRepo>();
            var Logger = new Mock<ILogger<OrdersController>>();
            Lrepo.Setup(repo => repo.GetById(1))
            .Returns(new List<Order>(){ new Order()
            {
                OrderId = 1,
                CustomerId = 2,
                ProductId=1,
                LocationId=1,
                Qty = 5,
                Location = new Location()
                {
                    Name="test"
                },
                Product=new Product()
                {
                    Name="test"
                },
                Customer=new Customer()
                {
                    FirstName="Bill",
                    LastName="Test"
                }
            }
            }); 
            var controller = new OrdersController(Lrepo.Object, Logger.Object);
            var result = (ViewResult)await controller.Details(1);
            Assert.IsType<OrderVM>(result.ViewData.Model);
        }
        [Fact]
        public async void OrderControllerCheckout()
        {
            var Lrepo = new Mock<IOrderRepo>();
            var Logger = new Mock<ILogger<OrdersController>>();
            var o = new List<Order>() {
                new Order() {
                OrderId = 1,
                CustomerId = 2,
                ProductId=1,
                LocationId=1,
                Qty = 5,
                Location = new Location()
                {
                    Name="test"
                },
                Product=new Product()
                {
                    Name="test"
                },
                Customer=new Customer()
                {
                    FirstName="Bill",
                    LastName="Test"
                }
                }
            };
            Lrepo.Setup(repo => repo.ValidateQty(o[0]))
            .Returns(true);
            Lrepo.Setup(repo => repo.Create(o)).Returns(111);
            var controller = new OrdersController(Lrepo.Object, Logger.Object);
            var request = new CheckoutVM() {
                CustomerId = 1,
                Items = new List<CheckoutItemVM>()
                {
                    new CheckoutItemVM()
                    {
                        LocationId = 1,
                        ProductId = 1,
                        Qty = 1
                    }
                }
            };
        var result = controller.Checkout(request);
            Assert.IsType<JsonResult>(result);
        }
    }
}
