using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DataAccess.Repositories;

namespace App.Web
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepo _repo;

        public CustomersController(ICustomerRepo repo)
        {
            _repo = repo;
        }
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var customer = _repo.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            var data = Mapper.Map(customer);
            return View(data);
        }
        public async Task<IActionResult> Search(string SearchString)
        {
            var customer = _repo.Search(SearchString);
            if (customer == null)
            {
                return NotFound();
            }
            var data = Mapper.Map(customer);
            return View(data);
        }
        /*
        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["DefaultLocationId"] = new SelectList(_context.Location, "Id", "Name");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DefaultLocationId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DefaultLocationId"] = new SelectList(_context.Location, "Id", "Name", customer.DefaultLocationId);
            return View(customer);
        }
        */
        // GET: Customers/Edit/5
    }
}
