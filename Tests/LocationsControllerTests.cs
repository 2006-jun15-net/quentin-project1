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
    public class LocationsControllerTests
    {
        [Fact]
        public async void LocationsDetails()
        {
                // Arrange
                var Lrepo = new Mock<ILocationRepo>();
                var Logger = new Mock<ILogger<LocationsController>>();
                Lrepo.Setup(repo => repo.GetById(1))
                .Returns(new Location(){
                    Id=1,
                    Name="Location"
                }
                );
                var controller = new LocationsController(Lrepo.Object, Logger.Object);
                var result = (ViewResult)await controller.Details(1);
                Assert.IsType<LocationVM>(result.ViewData.Model);
            }
    }
}
