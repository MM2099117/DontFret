using DFStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DFStore.Controllers
{
    public class StoreController : Controller
    {
        DontFretEntities db = new DontFretEntities();

        /// <summary>
        /// Controller Function for loading the main store page
        /// </summary>
        /// <returns>view with list of categories</returns>
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        /// <summary>
        /// Controller Function for browsing products + santizing category selection
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Products View</returns>
        public ActionResult Browse(string category)
        {
            var CategoryModel = db.Categories.Include("Products")
                .Single(c => c.CategoryName == category);

            return View(CategoryModel);
        }

        /// <summary>
        /// Controller Function for looking at individual products
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product Details view</returns>
        public ActionResult Details(int id)
        {
            var product = db.Products.Find(id);

            return View(product);
        }


    } 
}