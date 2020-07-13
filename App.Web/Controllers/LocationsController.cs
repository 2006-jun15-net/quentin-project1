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
    public class LocationsController : Controller
    {
        private readonly ILocationRepo _repo;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(ILocationRepo repo, ILogger<LocationsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var location = _repo.GetById(id);
            var data = Mapper.Map(location);
            if (location == null)
            {
                _logger.LogWarning($"No Location Found with ID: {id}");
                return NotFound();
            }
            _logger.LogInformation($"Serving Location with ID:{id}");
            return View(data);
        }
    }
}
