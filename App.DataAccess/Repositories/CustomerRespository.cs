using App.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepo
    {
        private DbContext _context;
        public Customer Add(Customer c)
        {
            var C = _context.Set<Entities.Customer>();
            C.Add(c);
           _context.SaveChanges();
            return GetById(c.Id);
        }
        public Customer GetById(int id)
        {
            return _context.Set<Entities.Customer>()
            .Where(x => x.Id == id)
            .Include(x => x.Order)
            .ThenInclude(o => o.Product)
            .Include(x=>x.DefaultLocation)
            .FirstOrDefault();
        }
        public List<Customer> Search(string searchstring)
        {
            if (searchstring == "") return _context.Set<Entities.Customer>()
                    .Include(x=>x.DefaultLocation)
                    .ToList();
            else return _context.Set<Entities.Customer>().Where(x => (x.FirstName.ToLower() + x.LastName.ToLower()).Contains(searchstring.ToLower()))
                    .Include(x => x.DefaultLocation)
                    .ToList();
        }
        public CustomerRepository(MyDBContext context)
        {
            this._context = context;
        }
    }
}
