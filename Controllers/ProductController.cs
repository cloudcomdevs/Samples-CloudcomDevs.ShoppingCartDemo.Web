using CloudComDevs.ShoppingCartDemo.Web.Models;
using CloudComDevs.ShoppingCartDemo.Web.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CloudComDevs.ShoppingCartDemo.Web.Controllers
{

    public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
    {
        public RequireRequestValueAttribute(string valueName)
        {
            ValueName = valueName;
        }
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return (controllerContext.HttpContext.Request[ValueName] != null);
        }
        public string ValueName { get; private set; }
    }

    public class ProductController : Controller
    {
        ProductRepository repository = new ProductRepository();
        //
        // GET: /Product/
        
        public ActionResult Index()
        {           
           IList<Product> products = repository.GetLatestItems();
           ViewBag.Title = "Latest Products";
            return View(products);
        }

        [RequireRequestValue("id")]
        public ActionResult Index(int id)
        {
            IList<Product> products = repository.GetProductsByCategory(id);
            ViewBag.Title = "Products by category";
            return View(products);
        }

        [RequireRequestValue("query")]
        public ActionResult Index(string query)
        {
            IList<Product> products = repository.GetByName(query);
            ViewBag.Title = "search results for - " + query;
            if (products.Count == 0)
            {
                ModelState.AddModelError("CustomError", "No results found for your search");
            }
            return View(products);
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            Product product = repository.Get(id);
            return View(product);
        }

        //
        // GET: /Product/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Message = "Create new product";
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles="Admin")]
        public ActionResult Create(Product model, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                if (file.ContentLength > 0)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }
                    model.PostedDate = DateTime.Now;
                    model.Picture = imageData;
                    model.ImagePath = file.FileName;
                    
                }
                model.Remaining = model.Quantity;
                repository.Add(model);
                repository.SaveChanges();
                ViewBag.Message = "Save successfull";
                
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("CustomMessage", ex.Message);
               
            }
            return View();
        }      

        [HttpPost]
        public ActionResult Delete(int id)
        {
            
                repository.Delete(id);
                repository.SaveChanges();
                return View();
            
        }
    }
}
