using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DataAccess.Entities;
using App.DataAccess.Repositories;
namespace App.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepo _repo;
        public OrdersController(IOrderRepo repo)
        {
            _repo = repo;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View();
        }
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = _repo.GetById(id);
            var vm = Mapper.Map(order);
            
            if (order == null)
            {
                return NotFound();
            }

            return View(vm);
        }
    }
}
