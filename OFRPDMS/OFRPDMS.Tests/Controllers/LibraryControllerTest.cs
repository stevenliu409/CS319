using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OFRPDMS;
using OFRPDMS.Models;
using OFRPDMS.Areas.Staff.Controllers;
using OFRPDMS.Account;
using OFRPDMS.Repositories;

using Moq;

namespace OFRPDMS.Tests.Controllers
{
    [TestClass]
    public class LibraryControllerTest
    {
        [TestMethod]
        public void Index_WithLoggedInAdmin()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<ILibraryRepository> libraryRepo = new Mock<ILibraryRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var libraryResources = new[] { new LibraryResource { Id=1, CenterId=1, Broken=false, Name="item" } };

            // center id 1 returns the item
            libraryRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(libraryResources.Where(l => l.CenterId == 1));
            libraryRepo.Setup(r => r.FindAllWithCenterId(2)).Returns(libraryResources.Where(l => l.CenterId == 2));
            repoService.SetupGet(r => r.libraryRepo).Returns(() => libraryRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            LibraryController controller = new LibraryController(accountService.Object, repoService.Object);

            // Act
            ActionResult result = controller.Index() as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<LibraryResource>));

            CollectionAssert.AreEqual(libraryResources.ToList(), ((IEnumerable<LibraryResource>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_WithLoggedInAdminAndDifferentCenterId()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<ILibraryRepository> libraryRepo = new Mock<ILibraryRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 2);

            var libraryResources = new[] { new LibraryResource { Id = 1, CenterId = 1, Broken = false, Name = "item" } };

            // center id 1 returns the item
            libraryRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(libraryResources.Where(l => l.CenterId == 1));
            libraryRepo.Setup(r => r.FindAllWithCenterId(2)).Returns(libraryResources.Where(l => l.CenterId == 2));
            repoService.SetupGet(r => r.libraryRepo).Returns(() => libraryRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            LibraryController controller = new LibraryController(accountService.Object, repoService.Object);

            // Act
            ActionResult result = controller.Index() as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<LibraryResource>));

            CollectionAssert.AreEqual(libraryResources.Where(l => l.CenterId == 2).ToList(), ((IEnumerable<LibraryResource>)asViewResult.Model).ToList());
        }
    }
}
