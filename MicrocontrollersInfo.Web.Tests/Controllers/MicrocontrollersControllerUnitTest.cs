using MicrocontrollersInfo.Entity;
using MicrocontrollersInfo.Repositories.Interfaces;
using MicrocontrollersInfo.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MicrocontrollersInfo.Web.Tests.Controllers
{
    [TestClass]
    public class MicrocontrollersControllerUnitTest
    {
        private static List<HousingType> housingTypes = new List<HousingType>() {
            new HousingType("Dual In-line Package", null) { Id = 1 },
            new HousingType("Thin Quad Flat Pack", null) { Id = 2 },
            new HousingType("Quad Flat Package", null) { Id = 3 },
        };

        private List<Microcontrollers> microcontrollers = new List<Microcontrollers>();

        [TestInitialize]
        public void Init()
        {
            microcontrollers.Clear();
            microcontrollers.Add(new Microcontrollers("Atiny11L", housingTypes.First(e => e.name == "Dual In-line Package"))
            {
                Id = 1,
            });
            microcontrollers.Add(new Microcontrollers("AT90S2313",
                housingTypes.First(e => e.name == "Quad Flat Package"))
            {
                Id = 2,
            });
            microcontrollers.Add(new Microcontrollers("Atiny15L",
               housingTypes.First(e => e.name == "Dual In-line Package"))
            {
                Id = 3,
            });
        }

        [TestMethod]
        //public void TestMethod1() {
        public void MicrocontrollersByHousingTypeInfo_ResultIsViewResult()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            var controller = new MicrocontrollersController()
            {
                UoW = uowMock.Object,
            };

            var result = controller.MicrocontrollersByHousingTypeInfo();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void MicrocontrollersByHousingTypeInfo_ViewBagAreEqualParam_True()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            var controller = new MicrocontrollersController()
            {
                UoW = uowMock.Object,
            };
            string categoryName = "Thin Quad Flat Pack";

            var result = controller
                .MicrocontrollersByHousingTypeInfo(categoryName) as ViewResult;
            var selectedCategoryName = result.ViewBag
                .SelectedCategoryName as string;

            Assert.AreEqual(categoryName, selectedCategoryName);
        }

        [TestMethod]
        public void MicrocontrollersByHousingTypeInfo_ModelIsEnumerableOfMicrocontrollers()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            var controller = new MicrocontrollersController()
            {
                UoW = uowMock.Object,
            };

            var result = controller.MicrocontrollersByHousingTypeInfo() as ViewResult;
            var model = result.Model;

            Assert.IsInstanceOfType(model, typeof(IEnumerable<Microcontrollers>));
        }

        [TestMethod]
        public void MicrocontrollersByHousingTypeInfo_NoParam_ModelContainsAllObjects()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            var controller = new MicrocontrollersController()
            {
                UoW = uowMock.Object,
            };

            var result = controller.MicrocontrollersByHousingTypeInfo() as ViewResult;
            var model = result.Model as IEnumerable<Microcontrollers>;

            Assert.AreEqual(microcontrollers.Count, model.Count());
        }

        [TestMethod]
        public void MicrocontrollersByHousingTypeInfo_ParamALL_VALUES_ModelContainsAllObjects()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            var controller = new MicrocontrollersController()
            {
                UoW = uowMock.Object,
            };

            var result = controller.MicrocontrollersByHousingTypeInfo(
                RouteConfig.ALL_VALUES) as ViewResult;
            var model = result.Model as IEnumerable<Microcontrollers>;

            Assert.AreEqual(microcontrollers.Count, model.Count());
        }

        [TestMethod]
        public void MicrocontrollersByHousingTypeInfo_ParamEasternEurope_ModelContains2Object()
        {
            var uowMock = new Mock<IInfoUnitOfWork>();
            uowMock.Setup(obj => obj.MicrocontrollersRepository.GetAll())
                .Returns(microcontrollers);
            uowMock.Setup(obj => obj.HousingTypesRepository.GetAll())
                .Returns(housingTypes);
            var controller = new MicrocontrollersController()
            {
                UoW = uowMock.Object,
            };
            string categoryName = "Dual In-line Package";

            var result = controller
                .MicrocontrollersByHousingTypeInfo(categoryName) as ViewResult;
            var model = result.Model as IEnumerable<Microcontrollers>;

            Assert.AreEqual(2, model.Count());
        }
    }
}
