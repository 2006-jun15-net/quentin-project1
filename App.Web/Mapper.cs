using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DataAccess.Entities;
using App.Web.Models.ViewModels;
namespace App.Web
{
    public static class Mapper
    {
        public static List<InventoryVM> Map(List<Inventory> I)
        {
            var r = new List<InventoryVM>();
            foreach(var i in I)
            {
                var _ = new InventoryVM();
                _.ProductName = i.Product.Name;
                _.ProductId = i.ProductId;
                _.Price = i.Product.Price;
                _.Qty = i.Qty;
                _.Location = i.Location.Name;
                _.LocationId = i.LocationId;
                r.Add(_);
            }
            return r;
        }
        public static OrderVM Map(List<Order> O)
        {
            OrderVM o = new OrderVM();
            o.Id = O[0].OrderId;
            o.CustomerName = $"{O[0].Customer.FirstName} {O[0].Customer.LastName}";
            o.Items = new List<OrderItemVM>();

                foreach (Order i in O)
                {
                    o.Items.Add(new OrderItemVM()
                    {
                        ProductName = i.Product.Name,
                        Qty = i.Qty,
                        Location = i.Location.Name
                    });
                }
            return o;
            }
        public static LocationVM Map(Location L)
        {
            var _ = new LocationVM();
            _.Name = L.Name;
            _.Orders = new List<OrderVM>();
            _.Inventory = new List<InventoryVM>();
            foreach (var i in L.Order)
            {
                var o = new OrderVM()
                {
                    CustomerName = i.Customer.FirstName + " " + i.Customer.LastName,
                    CustomerId = i.CustomerId,
                    Id = i.OrderId
                };
                _.Orders.Add(o);
            }
            foreach (var i in L.Inventory)
            {
                var o = new InventoryVM()
                {
                    ProductName = i.Product.Name,
                    ProductId = i.ProductId,
                    Qty = i.Qty,
                    Price = i.Product.Price,
                    LocationId = i.LocationId
                };
                
                _.Inventory.Add(o);
            }
            return _;
        }
        public static CustomerVM Map(Customer c)
        {
            var _ = new CustomerVM();
            _.FirstName = c.FirstName; 
            _.LastName  = c.LastName;
            _.OrderHistory = new List<int>();
            _.Id = c.Id;
            _.LocationName = c.DefaultLocation.Name;
            foreach (var i in c.Order)
            {
                if (_.OrderHistory.IndexOf(i.OrderId) == -1)
                {
                    _.OrderHistory.Add(i.OrderId);
                }
            }
            return _;
        }
        public static Customer Map(CustomerVM c)
        {
            var _ = new Customer();
            _.FirstName = c.FirstName;
            _.LastName = c.LastName;
            _.DefaultLocationId = c.LocationId;
            return _;
        }
        public static List<CustomerVM> Map(List<Customer> c)
        {
            var _ = new List<CustomerVM>();
            foreach (var i in c)
            {
                var C = new CustomerVM();
                C.FirstName = i.FirstName; 
                C.LastName = i.LastName;
                C.Id = i.Id;
                C.LocationName = i.DefaultLocation == null ? "none" : i.DefaultLocation.Name;
                _.Add(C);
            }
            return _;
        }
        public static List<Order> Map(CheckoutVM c)
        {
            var CustomerId = c.CustomerId;
            var _ = new List<Order>();
            foreach (var i in c.Items)
            {
                var o = Map(i);
                o.CustomerId = CustomerId; 
                _.Add(o);
            }
            return _;
        }
        public static Order Map(CheckoutItemVM c)
        {
            return new Order()
            {
                ProductId = c.ProductId,
                LocationId = c.LocationId,
                Qty = c.Qty
            };
        }
    }
    }
