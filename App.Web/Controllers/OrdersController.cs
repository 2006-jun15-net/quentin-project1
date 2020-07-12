using App.DataAccess.Entities;
using App.DataAccess.Repositories;
using App.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.DataAccess.Entities;
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
        public JsonResult Checkout([FromBody]CheckoutVM data)
        {
            var order = Mapper.Map(data);
            var failedOrders = new List<Order>();
            for(int i = 0; i < order.Count ; i++)  
            {
                if (_repo.ValidateQty(order[i]) != true) {
                    failedOrders.Add(order[i]);
                    order.RemoveAt(i);
                   }
            }
            var oid = _repo.Create(order);
            return Json(
                new CheckoutConfirmationVM()
                {
                    OrderNumber = oid,
                    FailedOrders = failedOrders
                });
        }
    }
}
