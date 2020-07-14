using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.Web.Models.ViewModels;
using Newtonsoft.Json;
using App.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace App.Web.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private readonly IInventoryRepo _Irepo;
        private readonly ICustomerRepo _Crepo;
        private readonly ILogger<CartController> _logger;
        public CartController(IInventoryRepo irepo, ICustomerRepo crepo, ILogger<CartController> logger)
        {
            _Irepo = irepo;
            _Crepo = crepo;
            _logger = logger;
        }
        public ActionResult Index(string c)
        {
        var CartItems = JsonConvert.DeserializeObject<List<CartVM>>(c);
            decimal total = 0.0M;
            foreach(var i in CartItems)
            {
               var l = _Irepo.Find(i.ProductId, i.LocationId);
                i.ProductName = l.Product.Name;
                i.LocationName = l.Location.Name;
                i.Price = l.Product.Price;
                if (i.Qty > l.Qty) {
                    i.Qty = l.Qty;
                }
                i.Stock = l.Qty;
                total += i.Total;
            }
            ViewData["OrderTotal"] = total;
            ViewData["Customers"] = _Crepo.Search("")
            .Select(n => new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.FirstName+" "+n.LastName
            }).ToList();
            _logger.LogInformation("Getting Cart {Cart}", c);
            return View(CartItems);
        }
    }
}