using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OFRPDMS;
using OFRPDMS.Models;
using OFRPDMS.Areas.Staff.Controllers;
using OFRPDMS.Account;
using OFRPDMS.Repositories;
using OFRPDMS.Utils;

using Moq;

namespace OFRPDMS.Tests.Controllers
{
    [TestClass]
    public class PrimaryGuardianControllerTest
    {
        [TestMethod]
        public void Index_LoggedIn()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] { new PrimaryGuardian { Id = 1, CenterId = 1 }, new PrimaryGuardian { Id = 2, CenterId = 1, FirstName = "Anne" } };
            var pgs2 = new[] { new PrimaryGuardian { Id = 3, CenterId = 2, FirstName = "John" } };
            var pgs1Children = new[] { new Child { Id = 1, PrimaryGuardianId = pgs1[0].Id }, new Child { Id = 2, PrimaryGuardianId = pgs1[0].Id } };
            var pgs2Children = new[] { new Child { Id = 3, PrimaryGuardianId = pgs2[0].Id, FirstName = "Johnny" } };
            var pgs1SecGuardian = new[] { new SecondaryGuardian { Id = 1, PrimaryGuardianId = pgs1[0].Id } };
            pgs1[0].SecondaryGuardians = pgs1SecGuardian;
            pgs1[0].Children = pgs1Children;
            pgs2[0].Children = pgs2Children;

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(2)).Returns(pgs2);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("GET");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Index(null, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEquivalent(pgs1.ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }


        [TestMethod]
        public void Index_LoggedInAndDifferentCenterId()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 2
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 2);

            var pgs1 = new[] { new PrimaryGuardian { Id = 1, CenterId = 1 }, new PrimaryGuardian { Id = 2, CenterId = 1, FirstName = "Anne" } };
            var pgs2 = new[] { new PrimaryGuardian { Id = 3, CenterId = 2, FirstName = "John" } };
            var pgs1Children = new[] { new Child { Id = 1, PrimaryGuardianId = pgs1[0].Id }, new Child { Id = 2, PrimaryGuardianId = pgs1[0].Id } };
            var pgs2Children = new[] { new Child { Id = 3, PrimaryGuardianId = pgs2[0].Id, FirstName = "Johnny" } };
            var pgs1SecGuardian = new[] { new SecondaryGuardian { Id = 1, PrimaryGuardianId = pgs1[0].Id } };
            pgs1[0].SecondaryGuardians = pgs1SecGuardian;
            pgs1[0].Children = pgs1Children;
            pgs2[0].Children = pgs2Children;

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(2)).Returns(pgs2);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("GET");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Index(null, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEquivalent(pgs2.ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }



        [TestMethod]
        public void Index_WithSearchString()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] { new PrimaryGuardian { Id = 1, CenterId = 1, FirstName = "John" }, new PrimaryGuardian { Id = 2, CenterId = 1, FirstName = "Anne" } };
            var pgs1Children = new[] { new Child { Id = 1, PrimaryGuardianId = pgs1[0].Id }, new Child { Id = 2, PrimaryGuardianId = pgs1[0].Id } };
            var pgs1SecGuardian = new[] { new SecondaryGuardian { Id = 1, PrimaryGuardianId = pgs1[0].Id } };
            pgs1[0].SecondaryGuardians = pgs1SecGuardian;
            pgs1[0].Children = pgs1Children;

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string searchString = "john";
            ActionResult result = controller.Index(null, null, searchString, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(searchString, asViewResult.ViewBag.CurrentFilter);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            // searched by john, but search is case-insensitive so expect to return only John
            CollectionAssert.AreEquivalent(pgs1.Where(p => p.FirstName == "John").ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }


        [TestMethod]
        public void Index_OrderByDefaultLastName()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Index(null, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.LastName).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByFirstName()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.FirstName).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByLastName()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc1";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.LastName).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByEmail()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc2";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.Email).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByCountry()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc3";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.Country).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByLanguage()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc4";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.Language).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByPostalCodePrefix()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc5";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.PostalCodePrefix).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByAllergies()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc6";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.Allergies).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByPhone()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc7";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.Phone).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByChildrenCount()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            pgs1[0].Children = pgs1Children;
            pgs1[0].SecondaryGuardians = pgs1SecGuardian;

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc8";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.Children.Count).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderBySecondaryGuardianCount()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            pgs1[0].Children = pgs1Children;
            pgs1[0].SecondaryGuardians = pgs1SecGuardian;

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Name desc9";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.SecondaryGuardians.Count).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByDateCreated()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            pgs1[0].Children = pgs1Children;
            pgs1[0].SecondaryGuardians = pgs1SecGuardian;

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Date";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.DateCreated).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }

        [TestMethod]
        public void Index_OrderByDateCreatedDescending()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new[] {
                Util.RandomPrimaryGuardian(1, 1),
                Util.RandomPrimaryGuardian(2, 2),
                Util.RandomPrimaryGuardian(3, 1) };
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            pgs1[0].Children = pgs1Children;
            pgs1[0].SecondaryGuardians = pgs1SecGuardian;

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            string order = "Date desc";
            ActionResult result = controller.Index(order, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.AreEqual(order, asViewResult.ViewBag.CurrentSort);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            CollectionAssert.AreEqual(pgs1.OrderByDescending(p => p.DateCreated).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }


        [TestMethod]
        public void Index_FirstPageContainsFirst10Items()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new List<PrimaryGuardian>();
            for (int i = 0; i < 15; i++)
            {
                pgs1.Insert(i, (Util.RandomPrimaryGuardian(i+1, 1)));
            }
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Index(null, null, null, null) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            int j = 0;
            CollectionAssert.AreEqual(pgs1.OrderBy(p => p.LastName).TakeWhile(p => {
                bool have10 = j < 10;
                j++;
                return have10;
            }).ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }


        [TestMethod]
        public void Index_SecondPageContainsNext10Items()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);

            var pgs1 = new List<PrimaryGuardian>();
            for (int i = 0; i < 15; i++)
            {
                pgs1.Insert(i, (Util.RandomPrimaryGuardian(i + 1, 1)));
            }
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindAllWithCenterId(1)).Returns(pgs1);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Index(null, null, null, 2) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(IEnumerable<PrimaryGuardian>));

            int j = 0;
            
            var expected = pgs1.OrderBy(p => p.LastName).TakeWhile((p, i) =>
                {
                    bool take = j < 10 && i >= 0;
                    if (take) j++;
                    return take;
                });

            CollectionAssert.AreEqual(expected.ToList(), ((IEnumerable<PrimaryGuardian>)asViewResult.Model).ToList());
        }


        [TestMethod]
        public void Details_LoggedInAdmin_ShouldReturnAPrimaryGuardian()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);
            accountService.Setup(a => a.GetRolesForUser()).Returns(new string[] { "Administrators" });

            var pgs1 = new List<PrimaryGuardian>();
            for (int i = 0; i < 15; i++)
            {
                pgs1.Insert(i, (Util.RandomPrimaryGuardian(i + 1, 1)));
            }
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindByIdAndCenterId(7, 1)).Returns(pgs1[6]);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Details(7) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsTrue(asViewResult.ViewBag.IsAdmin);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(PrimaryGuardian));

            Assert.AreEqual(pgs1[6], (PrimaryGuardian)asViewResult.Model);
        }


        [TestMethod]
        public void Details_LoggedInStaff_ShouldReturnAPrimaryGuardian()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);
            accountService.Setup(a => a.GetRolesForUser()).Returns(new string[] { "Staff" });

            var pgs1 = new List<PrimaryGuardian>();
            for (int i = 0; i < 15; i++)
            {
                pgs1.Insert(i, (Util.RandomPrimaryGuardian(i + 1, 1)));
            }
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindByIdAndCenterId(7, 1)).Returns(pgs1[6]);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Details(7) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsFalse(asViewResult.ViewBag.IsAdmin);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(PrimaryGuardian));

            Assert.AreEqual(pgs1[6], (PrimaryGuardian)asViewResult.Model);
        }


        [TestMethod]
        public void Details_WithInvalidCenterId_ShouldReturnNull()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 2
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 2);
            accountService.Setup(a => a.GetRolesForUser()).Returns(new string[] { "Staff" });

            var pgs1 = new List<PrimaryGuardian>();
            for (int i = 0; i < 15; i++)
            {
                pgs1.Insert(i, (Util.RandomPrimaryGuardian(i + 1, 1)));
            }
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindByIdAndCenterId(7, 1)).Returns(pgs1[6]);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Details(7) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsFalse(asViewResult.ViewBag.IsAdmin);

            Assert.IsNull((PrimaryGuardian)asViewResult.Model);
        }


        [TestMethod]
        public void EditGet_WithLoggedInAdmin_ShouldReturnAPrimaryGuardian()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);
            accountService.Setup(a => a.GetRolesForUser()).Returns(new string[] { "Administrators" });

            var pgs1 = new List<PrimaryGuardian>();
            for (int i = 0; i < 15; i++)
            {
                pgs1.Insert(i, (Util.RandomPrimaryGuardian(i + 1, 1)));
            }
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindByIdAndCenterId(7, 1)).Returns(pgs1[6]);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Edit(7) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsTrue(asViewResult.ViewBag.IsAdmin);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(PrimaryGuardian));

            Assert.AreEqual(pgs1[6], (PrimaryGuardian)asViewResult.Model);
        }


        [TestMethod]
        public void EditGet_WithLoggedInStaff_ShouldReturnAPrimaryGuardian()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);
            accountService.Setup(a => a.GetRolesForUser()).Returns(new string[] { "Staff" });

            var pgs1 = new List<PrimaryGuardian>();
            for (int i = 0; i < 15; i++)
            {
                pgs1.Insert(i, (Util.RandomPrimaryGuardian(i + 1, 1)));
            }
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindByIdAndCenterId(7, 1)).Returns(pgs1[6]);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Edit(7) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsFalse(asViewResult.ViewBag.IsAdmin);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(PrimaryGuardian));

            Assert.AreEqual(pgs1[6], (PrimaryGuardian)asViewResult.Model);
        }


        /*
        [TestMethod]
        public void EditPost_WithLoggedInStaff_ShouldReturnAPrimaryGuardian()
        {
            // Arrange
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            Mock<IRepositoryService> repoService = new Mock<IRepositoryService>();
            Mock<IPrimaryGuardianRepository> primaryGuardianRepo = new Mock<IPrimaryGuardianRepository>();
            Mock<ICenterRepository> centerRepo = new Mock<ICenterRepository>();

            // center id 1
            accountService.Setup(a => a.GetCurrentUserCenterId()).Returns(() => 1);
            accountService.Setup(a => a.GetRolesForUser()).Returns(new string[] { "Staff" });

            var pgs1 = new List<PrimaryGuardian>();
            for (int i = 0; i < 15; i++)
            {
                pgs1.Insert(i, (Util.RandomPrimaryGuardian(i + 1, 1)));
            }
            var pgs1Children = new[] { Util.RandomChild(1, pgs1[0].Id), Util.RandomChild(2, pgs1[0].Id) };
            var pgs1SecGuardian = new[] { Util.RandomSecondaryGuardian(1, pgs1[0].Id) };

            primaryGuardianRepo.Setup(r => r.FindByIdAndCenterId(7, 1)).Returns(pgs1[6]);
            repoService.SetupGet(r => r.primaryGuardianRepo).Returns(() => primaryGuardianRepo.Object);
            repoService.SetupGet(r => r.centerRepo).Returns(() => centerRepo.Object);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.HttpMethod).Returns("POST");

            var controllerContext = new Mock<HttpContextBase>();
            controllerContext.SetupGet(x => x.Request).Returns(request.Object);

            PrimaryGuardiansController controller = new PrimaryGuardiansController(accountService.Object, repoService.Object);
            controller.ControllerContext = new ControllerContext(controllerContext.Object, new RouteData(), controller);


            // Act
            ActionResult result = controller.Edit(7) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult asViewResult = (ViewResult)result;

            Assert.IsFalse(asViewResult.ViewBag.IsAdmin);

            Assert.IsInstanceOfType(asViewResult.ViewData.Model, typeof(PrimaryGuardian));

            Assert.AreEqual(pgs1[6], (PrimaryGuardian)asViewResult.Model);
        }
         */
    }
}
