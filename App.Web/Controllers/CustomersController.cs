using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DataAccess.Repositories;
using App.Web.Models.ViewModels;
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
        public async Task<IActionResult> Create(CustomerVM c)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map(c);
                data = _repo.Add(data);
                var d = Mapper.Map(data);
                return View(d);
            }
            return BadRequest(ModelState);
        }
    }
}
