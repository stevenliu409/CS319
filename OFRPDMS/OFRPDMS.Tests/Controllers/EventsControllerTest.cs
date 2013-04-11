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
using System.Web.Routing;

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
            ActionResult result = controller.Index(null,null) as ActionResult;

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
            ActionResult result = controller.Index(null,null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<Event>));

            IEnumerable<Event> expected = events.Where(e => e.CenterId == 2);
            CollectionAssert.AreEqual(expected.ToList(), ((IEnumerable<Event>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void edit_events()
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
            eventRepo.Setup(e => e.FindById(2)).Returns(() => events[1]);
            eventRepo.Setup(e => e.FindById(1)).Returns(() => events[0]);

            EventsController controller = new EventsController(accountService.Object, repoService.Object);


            // Act
            ActionResult result = controller.Edit(1) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(Event));

            Assert.AreEqual(events[0], asViewResult.Model);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);

        }
        [TestMethod]
        public void delete_events() {

            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 2);

            var events = new[] {
                new Event { Id = 1, CenterId = 1, Date = System.DateTime.Now },
                new Event { Id = 2, CenterId = 2, Date = System.DateTime.Now } };

            repoService.SetupGet(r => r.eventRepo).Returns(() => eventRepo.Object);
            eventRepo.Setup(e => e.FindByIdAndCenterId(2,2)).Returns(() => events[1]);
            eventRepo.Setup(e => e.FindByIdAndCenterId(1,1)).Returns(() => events[0]);

            EventsController controller = new EventsController(accountService.Object, repoService.Object);
            ActionResult result = controller.Delete(1) as ActionResult;

            // Assert
//            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

          //  RedirectToRouteResult asRedirectResult = (RedirectToRouteResult)result;

            //StringAssert.Equals(asRedirectResult.RouteValues["action"], "Index");

        }
    }
}
