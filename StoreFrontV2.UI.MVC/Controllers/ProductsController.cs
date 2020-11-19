using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFrontV2.DATA.EF;
using System.Data.Entity;

namespace StoreFrontV2.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Department).Include(p => p.ProductStatu).Include(p => p.Shipper).Include(p => p.Employee);
            return View(products.ToList());
        }
    }
}