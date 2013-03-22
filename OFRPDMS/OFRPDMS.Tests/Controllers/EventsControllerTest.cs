using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OFRPDMS;
using OFRPDMS.Models;
using OFRPDMS.Repositories;
using OFRPDMS.Areas.Staff.Controllers;
using OFRPDMS.Account;

using Moq;

namespace OFRPDMS.Tests.Controllers
{
    [TestClass]
    public class EventsControllerTest
    {

        [TestMethod]
        public void Index_WithLoggedInAdmin()
        {

            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var events = new[] {
                new Event { Id = 1, CenterId = 1, Date = System.DateTime.Now },
                new Event { Id = 2, CenterId = 2, Date = System.DateTime.Now } };

            repoService.SetupGet(r => r.eventRepo).Returns(() => eventRepo.Object);
            eventRepo.Setup(e => e.FindAllWithCenterId(1)).Returns(() => events.Where(e => e.CenterId == 1));
            eventRepo.Setup(e => e.FindAllWithCenterId(2)).Returns(() => events.Where(e => e.CenterId == 2));

            EventsController controller = new EventsController(accountService.Object, repoService.Object);

            // Act
            ActionResult result = controller.Index() as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<Event>));

            IEnumerable<Event> expected = events.Where(e => e.CenterId==1);
            CollectionAssert.AreEqual(expected.ToList(), ((IEnumerable<Event>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_WithLoggedInAdminAndDifferentCenterId()
        {

            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 2);

            var events = new[] {
                new Event { Id = 1, CenterId = 1, Date = System.DateTime.Now },
                new Event { Id = 2, CenterId = 2, Date = System.DateTime.Now } };

            repoService.SetupGet(r => r.eventRepo).Returns(() => eventRepo.Object);
            eventRepo.Setup(e => e.FindAllWithCenterId(1)).Returns(() => events.Where(e => e.CenterId == 1));
            eventRepo.Setup(e => e.FindAllWithCenterId(2)).Returns(() => events.Where(e => e.CenterId == 2));

            EventsController controller = new EventsController(accountService.Object, repoService.Object);

            // Act
            ActionResult result = controller.Index() as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<Event>));

            IEnumerable<Event> expected = events.Where(e => e.CenterId == 2);
            CollectionAssert.AreEqual(expected.ToList(), ((IEnumerable<Event>)asViewResult.Model).ToList());
        }
    }
}
