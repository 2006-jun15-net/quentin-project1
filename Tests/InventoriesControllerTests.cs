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
    public class InventoriesControllerTests
    {
        private readonly Mock<IInventoryRepo> Lrepo = new Mock<IInventoryRepo>();
        private readonly Mock<ILogger<InventoriesController>> Logger = new Mock<ILogger<InventoriesController>>();
        [Fact]
        public async void InventoryControllerIndex()
        {
            // Arrange
            Lrepo.Setup(repo => repo.Get())
            .Returns(new List<Inventory>(){
            new Inventory()
            {
                ProductId=1,
                LocationId=2,
                Qty=2,
                Product=new Product()
                {
                    Name="test",
                    Price=22
                },
                Location=new Location()
                {
                    Name="test"
                }
            } });            
            var controller = new InventoriesController(Lrepo.Object, Logger.Object);
            var result = (ViewResult)await controller.Index();
            Assert.IsType<List<InventoryVM>>(result.ViewData.Model);
        }
    }
}
