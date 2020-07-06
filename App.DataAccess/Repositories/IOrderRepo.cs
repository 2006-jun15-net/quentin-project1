﻿using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
namespace App.DataAccess.Repositories
{
    public interface IOrderRepo
    {
        List<Order> GetById(int orderId);
        void Create(Order o);
        void Create(List<Order> o);
    }
}
