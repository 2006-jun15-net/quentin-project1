﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.Web.Models.ViewModels;
using Newtonsoft.Json;
using App.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Web.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private readonly IInventoryRepo _Irepo;
        private readonly ICustomerRepo _Crepo;
        public CartController(IInventoryRepo irepo, ICustomerRepo crepo)
        {
            _Irepo = irepo;
            _Crepo = crepo;
        }
        public ActionResult Index(string c)
        {
        var CartItems = JsonConvert.DeserializeObject<List<CartVM>>(c);
            foreach(var i in CartItems)
            {
               var l = _Irepo.Find(i.ProductId, i.LocationId);
                i.ProductName = l.Product.Name;
                i.LocationName = l.Location.Name;
            }
            ViewData["Customers"] = _Crepo.Search("")
            .Select(n => new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.FirstName+" "+n.LastName
            }).ToList();
            return View(CartItems);
        }
    }
}