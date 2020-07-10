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
        public Inventory Find(int pid, int lid)
        {
            return _context.Set<Entities.Inventory>()
                .Where(x => x.ProductId == pid && x.LocationId == lid)
                .Include(o => o.Product)
                .Include(o => o.Location)
                .FirstOrDefault();
        }
        public InventoryRepository(MyDBContext context)
        {
            this._context = context;
        }
    }
}
