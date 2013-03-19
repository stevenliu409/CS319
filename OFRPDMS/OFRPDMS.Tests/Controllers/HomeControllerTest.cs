using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OFRPDMS;
using OFRPDMS.Controllers;

namespace OFRPDMS.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ActionResult result = controller.Index() as ActionResult;

            // Assert
            //Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult redirectresult = (RedirectToRouteResult) result;
            Assert.IsTrue(redirectresult.RouteValues.Values.Contains("Account"));
            Assert.IsTrue(redirectresult.RouteValues.Values.Contains("LogOn"));
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
