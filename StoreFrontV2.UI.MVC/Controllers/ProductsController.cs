using System;
using System.Collections.Generic;
using System.Data;
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
            var products = db.Products.Include(p => p.Category).Include(p => p.Department).Include(p => p.Employee).Include(p => p.ProductStatu).Include(p => p.Shipper);
            return View(products.ToList());
        }

        // GET: Products/Details/5
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

        public ActionResult AddToCart(int qty, int ProductID)
        {
            //Create an empty shell for the local shopping cart variable
            //A Dictionary is a collection. It holds a key and a value per record
            //Local version of our cart
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check if session-cart exists; if so, use it to populate local version
            //By default a session will last for 20 minutes. After 20 minutes of inactivity the session will expire
            if (Session["cart"] != null)
            {
                //Session cart exists - put its items in local version, which is easier to work with. 
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //When we unbox session object to it's smaller more specific type, we use explicit casting.
            }
            else
            {
                //if session cart doesn't exist yet, we need to "instantiate it"
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }
            //We now have a local cart that's ready to add things to it.

            //Find the product they refereced by ID
            Product product = db.Products.Where(b => b.ProductID == ProductID).FirstOrDefault();

            if (product == null)
            {
                //If they sent in a bad ID, kick them back to some page to try again
                return RedirectToAction("Index");
            }
            else
            {
                //If book is valid, add the line-item to card
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //Put item in the local cart but if we already have that product as a cart-item, then update
                //the quantity instead. This is the main reason we use a dictionary
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }

                //Now update the session version of the cart so we can maintain info between requests
                Session["cart"] = shoppingCart;//No explicit casting needed because we're going from smaller to larger

                //Confirmation message goes into session to be available after the redirect
                Session["confirm"] = $"'{product.ProductName}' (Quanity: {qty}) added to cart.";
            }

            //We will update this to send them to the cart view
            return RedirectToAction("Index", "ShoppingCart");
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName");
            ViewBag.ProdStatusID = new SelectList(db.ProductStatus, "ProdStatusID", "StatusName");
            ViewBag.ShippingID = new SelectList(db.Shippers, "ShippingID", "CompanyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Description,CategoryID,ProdStatusID,UnitsSold,Price,ShippingID,DeptID,EmployeeID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName", product.DeptID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName", product.EmployeeID);
            ViewBag.ProdStatusID = new SelectList(db.ProductStatus, "ProdStatusID", "StatusName", product.ProdStatusID);
            ViewBag.ShippingID = new SelectList(db.Shippers, "ShippingID", "CompanyName", product.ShippingID);
            return View(product);
        }

        // GET: Products/Edit/5
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName", product.DeptID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName", product.EmployeeID);
            ViewBag.ProdStatusID = new SelectList(db.ProductStatus, "ProdStatusID", "StatusName", product.ProdStatusID);
            ViewBag.ShippingID = new SelectList(db.Shippers, "ShippingID", "CompanyName", product.ShippingID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,Description,CategoryID,ProdStatusID,UnitsSold,Price,ShippingID,DeptID,EmployeeID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName", product.DeptID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName", product.EmployeeID);
            ViewBag.ProdStatusID = new SelectList(db.ProductStatus, "ProdStatusID", "StatusName", product.ProdStatusID);
            ViewBag.ShippingID = new SelectList(db.Shippers, "ShippingID", "CompanyName", product.ShippingID);
            return View(product);
        }

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
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
