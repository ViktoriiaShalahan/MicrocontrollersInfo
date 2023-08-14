using Common.Repositories.Interfaces;
using MicrocontrollersInfo.Entity;
using MicrocontrollersInfo.Repositories.Interfaces;
using MicrocontrollersInfo.Web.Controllers;
using MicrocontrollersInfo.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MicrocontrollersInfo.Web.Tests.Controllers
{
    [TestClass]
    public class MicrocontrollersCrudControllerUnitTest
    {
        private List<HousingType> housingTypes= new List<HousingType>() {
            new HousingType("Dual In-line Package", null) { Id = 1 },
            new HousingType("Thin Quad Flat Pack", null) { Id = 2 },
            new HousingType("Quad Flat Package", null) { Id = 3 },
        };

        private List<Microcontrollers> microcontrollers = new List<Microcontrollers>();

        [TestInitialize]
        public void InitializeTest()
        {
            CreateMicrocontrollers();
        }

        private void CreateMicrocontrollers()
        {
            microcontrollers.Clear();
            microcontrollers.Add(new Microcontrollers ("Atiny11L",
                housingTypes.First(e => e.name == "Dual In-line Package" ), "DIP", 1, 2M)
            {
                Id = 1,
            });
            microcontrollers.Add(new Microcontrollers("AT90S2313",
                housingTypes.First(e => e.name == "Quad Flat Package"))
            {
                Id = 2,
                price = 200,
            });
            microcontrollers.Add(new Microcontrollers("Atiny15L",
                housingTypes.First(e => e.name == "Dual In-line Package"), "DIP", 70M)
            {
                Id = 3,
            });
            microcontrollers.Add(item: new Microcontrollers("AT90S8515",
                housingTypes.First(e => e.name == "Quad Flat Package"), "QFP", 65M)
            {
                Id = 4,
            });
        }

        [TestMethod]
        public void Index_Model_IsBrowsingModelCollection()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            var controller = new MicrocontrollersCrudController()
            {
                UoW = uowMock.Object,
            };

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsInstanceOfType(result.Model,
                typeof(IEnumerable<MicrocontrollersBrowsingModel>));
        }

        [TestMethod]
        public void CreateGet_Model_IsEditingModelObject()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.HousingTypesRepository.GetAll())
                .Returns(housingTypes);
            var controller = new MicrocontrollersCrudController()
            {
                UoW = uowMock.Object,
            };

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(MicrocontrollersEditingModel));
        }

        [TestMethod]
        public void CreateGet_ViewBag_housingTypeNameIsListOfSelectListItem()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            uowMock.Setup(obj => obj.HousingTypesRepository.GetAll())
                .Returns(housingTypes);
            var controller = new MicrocontrollersCrudController()
            {
                UoW = uowMock.Object,
            };

            ViewResult result = controller.Create() as ViewResult;
            var value = result.ViewBag.housingTypeName;

            Assert.IsInstanceOfType(value, typeof(List<SelectListItem>));
        }


        [TestMethod]
        public void CreateGet_ViewBag_ListCountAreEqualHousingTypeCount()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            uowMock.Setup(obj => obj.HousingTypesRepository.GetAll())
                .Returns(housingTypes);
            var controller = new MicrocontrollersCrudController()
            {
                UoW = uowMock.Object,
            };
            int expectedCount = housingTypes.Count();

            ViewResult result = controller.Create() as ViewResult;
            var selectList = result.ViewBag.housingTypeName as List<SelectListItem>;

            Assert.AreEqual(expectedCount, selectList.Count);
        }

        [TestMethod]
        public void CreatePost_Result_RedirectToActionIndex()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            uowMock.Setup(obj => obj.HousingTypesRepository.GetAll())
                .Returns(housingTypes);
            var controller = new MicrocontrollersCrudController()
            {
                UoW = uowMock.Object,
            };
            var model = new MicrocontrollersEditingModel()
            {
                housingTypeName = "Quad Flat Package"
            };

            ActionResult result = controller.Create(model);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var redirectResult = result as RedirectToRouteResult;
            Assert.AreEqual(redirectResult.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void CreatePost_UoW_SaveIsCalled()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            uowMock.Setup(obj => obj.HousingTypesRepository.GetAll())
                .Returns(housingTypes);
            var controller = new MicrocontrollersCrudController()
            {
                UoW = uowMock.Object,
            };
            var model = new MicrocontrollersEditingModel()
            {
                housingTypeName = "Dual In-line Package"
            };

            ActionResult result = controller.Create(model);

            uowMock.Verify(e => e.Save());
        }

        [TestMethod]
        public void CreatePost_Repository_AddIsCalled()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            var repMock = new Mock<IRepository<Microcontrollers>>();
            repMock.Setup(obj => obj.GetAll())
                .Returns(microcontrollers);
            uowMock.Setup(obj => obj.MicrocontrollersRepository)
                .Returns(repMock.Object);
            uowMock.Setup(obj => obj.HousingTypesRepository.GetAll())
                .Returns(housingTypes);
            var controller = new MicrocontrollersCrudController()
            {
                UoW = uowMock.Object,
            };
            var model = new MicrocontrollersEditingModel()
            {
                housingTypeName = "Thin Quad Flat Pack"
            };

            ActionResult result = controller.Create(model);

            repMock.Verify(e => e.Add(It.IsAny<Microcontrollers>()));
        }
        

        [TestMethod]
        public void CreatePost_TempData_KeysContains_message()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            uowMock.Setup(obj => obj.HousingTypesRepository.GetAll())
                .Returns(housingTypes);
            var controller = new MicrocontrollersCrudController()
            {
                UoW = uowMock.Object,
            };
            var model = new MicrocontrollersEditingModel()
            {
                Brand = "AT90S2313",
                housingTypeName = "Quad Flat Package"
            };

            ActionResult result = controller.Create(model);

            Assert.IsTrue(controller.TempData.Keys.Contains("message"));
        }

        [TestMethod]
        public void CreatePost_ModelStateIsNotValid_ReturnedViewResult()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            uowMock.Setup(obj => obj.HousingTypesRepository.GetAll())
               .Returns(housingTypes);
            var controller = new MicrocontrollersCrudController()
            {
                UoW = uowMock.Object,
            };
            var model = new MicrocontrollersEditingModel();
            controller.ModelState.AddModelError("", "error message");

            ActionResult result = controller.Create(model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(viewResult.Model, model);
        }

    }
}
