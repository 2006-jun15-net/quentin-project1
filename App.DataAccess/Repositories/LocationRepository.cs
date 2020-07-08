using App.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace App.DataAccess.Repositories 
{
    public class LocationRepository : ILocationRepo
    {
    private DbContext _context;
    public Location GetById(int id)
        {
            return _context.Set<Entities.Location>()
                .Include(x=>x.Inventory)
                .ThenInclude(x=>x.Product)
                .Include(x=>x.Order)
                .ThenInclude(x=>x.Customer)
                .Where(x => x.Id == id).FirstOrDefault();
        }
    public LocationRepository(MyDBContext context)
    {
        this._context = context;
    }
}
}
