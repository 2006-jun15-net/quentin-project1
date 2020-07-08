using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
namespace App.DataAccess.Repositories
{
    public class InventoryRepository : IInventoryRepo
    {
        private DbContext _context;
        public List<Inventory> Get()
        {
            return _context.Set<Entities.Inventory>()
                .Include(o => o.Product)
                .Include(o => o.Location)
                .ToList();
        }
        public InventoryRepository(MyDBContext context)
        {
            this._context = context;
        }
    }
}
