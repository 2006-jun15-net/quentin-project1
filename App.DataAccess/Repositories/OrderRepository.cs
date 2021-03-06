﻿using System;
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
            return _context.Set<Entities.Order>().Where(x => x.OrderId == orderId)
                .Include(o => o.Customer)
                .Include(o => o.Location)
                .Include(o => o.Product)
                .ToList();
        }
        public virtual int GetNewOrderId()
        {
            return _context.Set<Entities.Order>().Select(o => o.OrderId).Max();
        }
        public bool ValidateQty(Order o)
        {
           return _context.Set<Entities.Inventory>()
                .Count(x => x.ProductId == o.ProductId && x.LocationId == o.LocationId && x.Qty >= o.Qty)           
                == 0 ?
                false:
                true;
        }
        public void Create(Order o)
        {
            o.Date = o.Date == DateTime.MinValue ? DateTime.Now : o.Date;
            o.OrderId = o.OrderId == 0 ? GetNewOrderId() + 1 : o.OrderId;
            var I = _context.Set<Entities.Inventory>().First(x => x.ProductId == o.ProductId && x.LocationId == o.LocationId);
            if (I == null)
                {
                    throw new InvalidOperationException($"LocationID:{o.Location.Id} is currently out of stock of ProductID:{o.ProductId}");
                };
                if (I.Qty >= o.Qty)
                {
                    I.Qty -= o.Qty;
                    _context.Update(I);
                    _context.Set<Order>().Add(o);
                    _context.SaveChanges();
                }
                else throw new ArgumentException($"Value is too great for current Location. Location Inventory on hand is:{I.Qty}");
        }
        public int Create(List<Order> o)
        {
            int OrderId = GetNewOrderId() + 1;
            DateTime d = DateTime.Now;
                foreach (Order O in o)
                {
                    O.OrderId = OrderId;
                    O.Date = d;
                    Create(O);
                }
                return OrderId;
        }
        public OrderRepository(MyDBContext context)
        {
            this._context = context;
        }
        public List<Order> Search(Customer c)
        {
            return _context.Set<Entities.Order>().Where(x => x.CustomerId == c.Id).ToList();
        }
        public List<Order> Search(Location l)
        {
            return _context.Set<Entities.Order>().Where(x => x.LocationId == l.Id).ToList();
        }
    }
        
}
