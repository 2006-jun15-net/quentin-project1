using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
namespace App.DataAccess.Repositories
{
    public interface IOrderRepo
    {
        List<Order> GetById(int orderId);
        void Create(Order o);
        int Create(List<Order> o);
        List<Order> Search(Customer c);
        List<Order> Search(Location l);
        bool ValidateQty(Order o);
    }
}
