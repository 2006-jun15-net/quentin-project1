using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DataAccess.Repositories;
using App.Web.Models.ViewModels;
using Microsoft.Extensions.Logging;

namespace App.Web
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepo _repo;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerRepo repo, ILogger<CustomersController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var customer = _repo.GetById(id);
            if (customer == null)
            {
                _logger.LogWarning($"Customer Id {id} not found");
                return NotFound();
            }
            var data = Mapper.Map(customer);
            _logger.LogInformation($"Displaying customer info for id: { id}");
            return View(data);
        }
        public async Task<IActionResult> Search(string SearchString)
        {
            var customer = _repo.Search(SearchString);
            if (customer == null)
            {
                _logger.LogWarning($"Unable to find customer matching search: {SearchString}");
                return NotFound();
            }
            var data = Mapper.Map(customer);
            _logger.LogInformation($"Displaying search results for {SearchString}");
            return View(data);
        }
        public async Task<IActionResult> Create(CustomerVM c)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map(c);
                data = _repo.Add(data);
                var d = Mapper.Map(data);
                _logger.LogInformation($"Displaying customerid: {d.Id}");
                return View(d);
            }
            _logger.LogWarning($"Invalid Model state for customer creation");
            return BadRequest(ModelState);
        }
    }
}
