using Xunit;
using App.DataAccess.Entities;
using App.Web.Models.ViewModels;
using System.Collections.Generic;
using App.Web;

namespace Tests
{
    public class MappingTests
    {
        [Fact]
        public void CustomerEntityToVMConversionsTest()
        {
            var a = new Customer();
            var e = Mapper.Map(a);
            Assert.IsType<CustomerVM>(e);
        }
        [Fact]
        public void InventoryListEntityToVMConversionsTest()
        {
            var b = new List<Inventory>() { new Inventory() {
            Product = new Product()
            {
                Name="bla",
                Price=42
            },
            Qty=23,
            Location=new Location()
            {
                Name="Here",
            },
            LocationId=22
            } };
            var f = Mapper.Map(b);
            Assert.IsType<List<InventoryVM>>(f);
        }
        [Fact]
        public void OrderListEntityToVMConversionsTest()
        {
            var c = new List<Order>() { new Order() {
            OrderId=55,
            Customer=new Customer()
            {
                FirstName="Bill",
                LastName="Jones"
            },
            Product=new Product()
            {
                Name="Product"
            },
            Qty=55,
            Location=new Location()
            {
                Name="Here"
            }
            }
            };
            var g = Mapper.Map(c);
            Assert.IsType<OrderVM>(g);
        }
        [Fact]
        public void LocationEntityToVMConversionsTest()
        {
            var d = new Location();
            var h = Mapper.Map(d);
            Assert.IsType<LocationVM>(h);
        }

    }
}
