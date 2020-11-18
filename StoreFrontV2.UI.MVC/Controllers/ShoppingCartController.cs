using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFrontV2.DATA.EF;
using StoreFrontV2.UI.MVC.Models;

namespace StoreFrontV2.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();

                ViewBag.Message = "There are no items in your cart";
            }
            else
            {
                ViewBag.Message = null;
            }
            return View(shoppingCart);
        }

        public ActionResult UpdateCart(int productID, int qty)
        {
            //Get the cart of the session and put it into a local variable
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //Target the correct cartItem using BookID for key - then change the qty
            shoppingCart[productID].Qty = qty;

            //return local cart to the session and send them back to the shopping cart index
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            //get the cart of the session
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //remove the item
            shoppingCart.Remove(id);            

            return RedirectToAction("Index");
        }
    }
}