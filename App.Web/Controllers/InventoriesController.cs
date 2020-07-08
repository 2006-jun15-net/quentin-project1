using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DataAccess.Repositories;

namespace App.Web.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly IInventoryRepo _repo;

        public InventoriesController(IInventoryRepo repo)
        {
            _repo = repo;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            var data =_repo.Get();
            var formatted = Mapper.Map(data);
            return View(formatted);
        }
    }
}
