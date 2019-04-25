using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Models;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using WebApplication1.Controllers;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class VLTeaController
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new CS4PEEntities();
            var controller = new BubbleTeaController();
            var result = controller.Index();

            var view = result as ViewResult;
            Assert.IsNotNull(view);

            var model = view.Model as List<BubleTea>;
            Assert.IsNotNull(model);
            Assert.AreEqual(db.BubleTeas.Count(), model.Count);
        }
        [TestMethod]
        public void TestDetails()
        {
            var db = new CS4PEEntities();
            var item = db.BubleTeas.First();
            var controller = new BubbleTeaController();

            var result = controller.Details(item.id);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            var model = view.Model as BubleTea;
            Assert.IsNotNull(model);
            Assert.AreEqual(item.id, model.id);

            result = controller.Details(0);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));


        }
        [TestMethod]
        public void TestCreate()
        {


        }
        [TestMethod]
        public void TestCreateP()
        {
            var db = new CS4PEEntities();
            var model = new BubleTea
            {
                Name = "Tra sua VL",
                Price = 26000,
                Topping = "tran chau trang"
            };
            var controller = new BubbleTeaController();

            var result = controller.Create(model);
            var redirect = result as RedirectToRouteResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.RouteValues["action"]);
            var item = db.BubleTeas.Find(model.id);
            Assert.IsNotNull(item);
            Assert.AreEqual(model.Name, item.Name);
            Assert.AreEqual(model.Price, item.Price);
            Assert.AreEqual(model.Topping, item.Topping);
        }
    }
}
