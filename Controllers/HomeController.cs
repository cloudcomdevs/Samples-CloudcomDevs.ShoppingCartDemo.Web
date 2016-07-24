using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudComDevs.Data;

namespace CloudComDevs.ShoppingCartDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            //UnitOfWork unitOfWork = new UnitOfWork(new DefaultDbContext());
            //var results = unitOfWork.Brands.GetAll();

            //foreach (var item in results)
            //{
               
            //}

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
