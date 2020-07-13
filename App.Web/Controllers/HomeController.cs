using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Web.Models;
using App.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDBContext _context;
        public HomeController(ILogger<HomeController> logger, MyDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home page visited");
            return View();
        }

        public IActionResult Search()
        {
            _logger.LogInformation("Search page visited");
            return View();
        }
        public IActionResult Add()
        {
            ViewData["Locations"] = _context.Location
            .Select(n => new SelectListItem
            {
            Value = n.Id.ToString(),
            Text = n.Name.ToString()
            }).ToList();
            _logger.LogInformation("Add page visited");
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogWarning("Error page visited");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
