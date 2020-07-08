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
    public class LocationsController : Controller
    {
        private readonly ILocationRepo _repo;

        public LocationsController(ILocationRepo repo)
        {
            _repo = repo;
        }
        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var location = _repo.GetById(id);
            var data = Mapper.Map(location);
            if (location == null)
            {
                return NotFound();
            }

            return View(data);
        }
    }
}
