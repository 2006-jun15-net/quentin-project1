using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace App.Web.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly IInventoryRepo _repo;
        private readonly ILogger<InventoriesController> _logger;
        public InventoriesController(IInventoryRepo repo, ILogger<InventoriesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            var data =_repo.Get();
            var formatted = Mapper.Map(data);
            _logger.LogInformation("Displaying inventories");
            return View(formatted);
        }
    }
}
