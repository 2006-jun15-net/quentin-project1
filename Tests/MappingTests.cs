using Xunit;
using App.DataAccess.Entities;
using App.Web.Models.ViewModels;
using System.Collections.Generic;
using App.Web;
using System;

namespace Tests
{
    public class MappingTests
    {
        [Fact]
        public void CustomerEntityToVMConversionsTest()
        {
            var a = new Customer() { 
            DefaultLocation=new Location()
            };
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
        [Fact]
        public void CheckoutItemVMtoOrderConversionTest()
        {
            var d = new CheckoutItemVM();
            var h = Mapper.Map(d);
            Assert.IsType<Order>(h);
        }
        [Fact]
        public void CheckoutVMtoListOrderConversionTest()
        {
            var d = new CheckoutVM()
            {
                CustomerId = 1,
                Items = new List<CheckoutItemVM>() {
                    new CheckoutItemVM()
                    {
                        LocationId = 1,
                        ProductId = 1,
                        Qty = 3
                    }
                }
            };
            var h = Mapper.Map(d);
            Assert.IsType<List<Order>>(h);
        }

    }
}
