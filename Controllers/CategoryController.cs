using CloudComDevs.ShoppingCartDemo.Web.Models;
using CloudComDevs.ShoppingCartDemo.Web.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudComDevs.ShoppingCartDemo.Web.Controllers
{
    public class CategoryController : Controller
    {

        CategoryRepository repository = new CategoryRepository();
        //
        // GET: /Category/
       // [AutoMap
        public PartialViewResult Index()
        {
           IList<Category> category = repository.GetAll();
            if (category == null)
            {
                return PartialView("index", category);
            }
            else
            {
                return PartialView("index", category);// View(category);
            }           
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id)
        {
            Category category = repository.Get(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(category);
            }      
        }

        //
        // GET: /Category/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Category model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                repository.Add(model);
                repository.SaveChanges();
               // return  RedirectToAction("Index");
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    Category category = repository.Get(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(category);
        //    }      
        //}

        //
        // POST: /Category/Edit/5

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public ActionResult Edit(int id, Category model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid) return View(model);

        //        repository.Update(model);
        //        repository.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //
        // POST: /Category/Delete/5

        [HttpGet]
        [Authorize(Roles = "Admin")]        
        public ActionResult Delete(int id)
        {
            try
            {
                repository.Delete(id);
                repository.SaveChanges();
                return Redirect(Request.UrlReferrer.AbsoluteUri);
               // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
