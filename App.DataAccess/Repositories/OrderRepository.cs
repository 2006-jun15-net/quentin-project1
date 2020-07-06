using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepo
    {
        private DbContext _context;
        public List<Order> GetById(int orderId)
        {
            return _context.Set<Entities.Order>().Where(x => x.OrderId == orderId).ToList();
        }
        public virtual int GetNewOrderId()
        {
            return _context.Set<Entities.Order>().Select(o => o.OrderId).Max();
        }
        public void Create(Order o)
        {
            o.Date = o.Date == null ? DateTime.Now : o.Date;
            o.OrderId = o.OrderId == 0 ? GetNewOrderId() + 1 : o.OrderId;
            var Inventory = _context.Set<Inventory>();
                var I = Inventory
                    .Where(x => x.ProductId == o.ProductId && x.Location.Id == o.Location.Id)
                    .FirstOrDefault();
                if (I == null)
                {
                    throw new InvalidOperationException($"LocationID:{o.Location.Id} is currently out of stock of ProductID:{o.ProductId}");
                };
                if (I.Qty >= o.Qty)
                {
                    I.Qty -= o.Qty;
                    Inventory.Update(I);
                    _context.Set<Order>().Add(o);
                    _context.SaveChanges();
                }
                else throw new ArgumentException($"Value is too great for current Location. Location Inventory on hand is:{I.Qty}");
        }
        public void Create(List<Order> o)
        {
            int OrderId = GetNewOrderId() + 1;
            DateTime d = DateTime.Now;
            foreach(Order O in o)
            {
                O.OrderId = OrderId;
                O.Date = d;
                Create(O);
            }
        }
        public OrderRepository(MyDBContext context)
        {
            this._context = context;
        }
    }
        
    }
}
