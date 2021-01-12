using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontV2.DATA.EF;

namespace StoreFrontV2.UI.MVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Categorys
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        //// GET: Categorys/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category Category = db.Categorys.Find(id);
        //    if (Category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Category);
        //}

        //// GET: Categorys/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Categorys/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "DeptID,DeptName")] Category Category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Categorys.Add(Category);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(Category);
        //}

        //// GET: Categorys/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category Category = db.Categorys.Find(id);
        //    if (Category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Category);
        //}

        //// POST: Categorys/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "DeptID,DeptName")] Category Category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(Category).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(Category);
        //}

        //// GET: Categorys/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category Category = db.Categorys.Find(id);
        //    if (Category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Category);
        //}

        //// POST: Categorys/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Category Category = db.Categorys.Find(id);
        //    db.Categorys.Remove(Category);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();

            string confirmMessage = string.Format("Deleted department '{0}' from the database.", Department.DeptName);
            return Json(new { id = id, message = confirmMessage });
        }

        [HttpGet]
        public PartialViewResult DeptDetails(int id)
        {
            Department department = db.Departments.Find(id);
            return PartialView(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxCreate(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
            return Json(department);
        }

        [HttpGet]
        public PartialViewResult DepartmentEdit(int id)
        {
            Department department = db.Departments.Find(id);
            return PartialView(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(Category Category)
        {
            db.Entry(Category).State = EntityState.Modified;
            db.SaveChanges();
            return Json(Category);
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
