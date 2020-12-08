using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontV2.DATA.EF;
using StoreFrontV2.UI.MVC.Models;

namespace StoreFrontV2.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();
        
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductStatu).Include(p => p.Category).Include(p => p.Department).Include(p => p.Shipper);
            return View(products.ToList());
        }

        //GET Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        #region Custom Add-to-Cart Functionality (called from details view)
        public ActionResult AddToCart(int qty, int productID)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            if (Session["cart"] != null)
            {
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            }
            else
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            Product product = db.Products.Where(b => b.ProductID == productID).FirstOrDefault();

            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                CartItemViewModel item = new CartItemViewModel(qty, product);

                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }

                Session["cart"] = shoppingCart;

                Session["confirm"] = $"'{product.ProductName}' (Quantity: {qty}) added to card.";
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        #endregion

        //GET Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.ProductStatuID = new SelectList(db.ProductStatus, "ProductStatusID", "StatusName");
            ViewBag.CagetoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.ShipperID = new SelectList(db.Shippers, "ShipperID", "CompanyName");
            return View();
        }

        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Description,CategoryID,Price,UnitsSold,DeptID,EmployeeID,ProductStatuID,ShipperID")] Product product)
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", product.ProductID);
            ViewBag.ProductStatuID = new SelectList(db.ProductStatus, "ProductStatuID", "StatusName", product.ProductStatu);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName", product.DeptID);
            ViewBag.ShipperID = new SelectList(db.Shippers, "ShipperID", "CompanyName", product.Shipper);
            return View(product);
        }

        //GET Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", product.ProductID);
            ViewBag.ProductStatuID = new SelectList(db.ProductStatus, "ProductStatuID", "StatusName", product.ProductStatu);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName", product.DeptID);
            ViewBag.ShipperID = new SelectList(db.Shippers, "ShipperID", "CompanyName", product.Shipper);
            return View(product);
        }

        //POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
            "ProductID,ProductName,Description,CategoryID,Price,UnitsSold,DeptID,EmployeeID,ProductStatuID,ShipperID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", product.ProductID);
            ViewBag.ProductStatuID = new SelectList(db.ProductStatus, "ProductStatuID", "StatusName", product.ProductStatu);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName", product.DeptID);
            ViewBag.ShipperID = new SelectList(db.Shippers, "ShipperID", "CompanyName", product.Shipper);
            return View(product);
        }

        //GET Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}