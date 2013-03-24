using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OFRPDMS;
using OFRPDMS.Controllers;
using OFRPDMS.Repositories;
using OFRPDMS.Account;

namespace OFRPDMS.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_NotLoggedIn()
        {
            // Arrange
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            HomeController controller = new HomeController(accountService.Object, repoService.Object);

            accountService.Setup(a => a.RoleExists("Administrators")).Returns(() => true);
            accountService.Setup(a => a.RoleExists("Staff")).Returns(() => true);
            accountService.Setup(a => a.GetRolesForUser()).Returns(() => new string[] {"Administrators"} );

            // Act
            ActionResult result = controller.Index() as ActionResult;

            // Assert
            //Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult redirectresult = (RedirectToRouteResult) result;
            StringAssert.Equals(redirectresult.RouteValues["action"], "LogOn");
            StringAssert.Equals(redirectresult.RouteValues["controller"], "Account");
        }

        [TestMethod]
        public void Index_WithLoggedInAdmin()
        {
            // Arrange
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            HomeController controller = new HomeController(accountService.Object, repoService.Object);

            accountService.Setup(a => a.RoleExists("Administrators")).Returns(() => true);
            accountService.Setup(a => a.RoleExists("Staff")).Returns(() => true);
            accountService.Setup(a => a.GetRolesForUser()).Returns(() => new string[] { "Administrators" });

            // Act
            ActionResult result = controller.Index() as ActionResult;

            // Assert
            //Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult redirectresult = (RedirectToRouteResult)result;
            StringAssert.Equals(redirectresult.RouteValues["action"], "Index");
            StringAssert.Equals(redirectresult.RouteValues["controller"], "Admin");
        }

        [TestMethod]
        public void Index_WithLoggedInStaff()
        {
            // Arrange
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            HomeController controller = new HomeController(accountService.Object, repoService.Object);

            accountService.Setup(a => a.RoleExists("Administrators")).Returns(() => true);
            accountService.Setup(a => a.RoleExists("Staff")).Returns(() => true);
            accountService.Setup(a => a.GetRolesForUser()).Returns(() => new string[] { "Staff" });

            // Act
            ActionResult result = controller.Index() as ActionResult;

            // Assert
            //Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult redirectresult = (RedirectToRouteResult)result;
            StringAssert.Equals(redirectresult.RouteValues["action"], "Index");
            StringAssert.Equals(redirectresult.RouteValues["controller"], "Staff");
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
