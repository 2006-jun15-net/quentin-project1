using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
namespace App.DataAccess.Repositories
{
    public interface ICustomerRepo
    {
        Customer Add(Customer c);
        List<Customer> Search(string s);
        Customer GetById(int i);
    }
}
