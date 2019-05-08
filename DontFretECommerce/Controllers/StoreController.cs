using DontFretECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DontFretECommerce.Controllers
{
    public class StoreController : Controller
    {
        /// <summary>
        /// Controller Function for loading the main store page
        /// </summary>
        /// <returns>view with list of categories</returns>
        public ActionResult Index()
        {
            var category = new List<Category>
            {
                new Category { CategoryName = "Electric" },
                new Category { CategoryName = "Acoustic" },
                new Category { CategoryName = "Amplifiers" },
                new Category { CategoryName = "Accessories" }
            };
            return View(category);
        }

        /// <summary>
        /// Controller Function for browsing products + santizing category selection
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Products View</returns>
        public ActionResult Browse(string category)
        {
            var CategoryModel = new Category
            {
                CategoryName = category
            };

            return View(CategoryModel);
        }

        /// <summary>
        /// Controller Function for looking at individual products
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product Details view</returns>
        public ActionResult Details(int id)
        {
            var product = new Product
            {
                ProductName = "Product" + id
            };
            return View(product);
        }


    }
}