using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using WebApplication1.Models;
using System.Web.Http.Results;
using System.Threading.Tasks;

namespace WebApplication1.Tests.Controllers
{
    /// <summary>
    /// Сводное описание для UnitTest3
    /// </summary>
    [TestClass]
    public class UnitTest3
    {


        [TestMethod]
        public void GetAllNews_ShouldReturnAllProducts()
        {
            var testProducts = GetTestNews();
            var controller = new NewsTestController(testProducts);

            var result = controller.GetAllNews() as List<NewsTest>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var testProducts = GetTestNews();
            var controller = new NewsTestController(testProducts);

            var result = await controller.GetAllNewsAsync() as List<NewsTest>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        [TestMethod]
        public void GetProduct_ShouldReturnCorrectNews()
        {
            var testProducts = GetTestNews();
            var controller = new NewsTestController(testProducts);

            var result = controller.GetNews(3) as OkNegotiatedContentResult<NewsTest>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts[3].title, result.Content.title);
        }

        [TestMethod]
        public async Task GetNewsAsync_ShouldReturnCorrectNews()
        {
            var testProducts = GetTestNews();
            var controller = new NewsTestController(testProducts);

            var result = await controller.GetNewsAsync(4) as OkNegotiatedContentResult<NewsTest>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts[3].title, result.Content.title);
        }

        [TestMethod]
        public void GetNews_ShouldNotFindProduct()
        {
            var controller = new NewsTestController(GetTestNews());

            var result = controller.GetNews(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private List<NewsTest> GetTestNews()
        {
            var testProducts = new List<NewsTest>();
            testProducts.Add(new NewsTest { id = 1, title = "Demo1", short_description = "short", description = "description" });
            testProducts.Add(new NewsTest { id = 2, title = "Demo1", short_description = "short", description = "description" });
            testProducts.Add(new NewsTest { id = 3, title = "Demo1", short_description = "short", description = "description" });
            testProducts.Add(new NewsTest { id = 4, title = "Demo1", short_description = "short", description = "description" });

            return testProducts;
        }




        public UnitTest3()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: добавьте здесь логику теста
            //
        }
    }
}
