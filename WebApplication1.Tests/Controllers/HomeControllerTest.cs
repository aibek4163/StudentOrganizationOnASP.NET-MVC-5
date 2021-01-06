using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController1 controller = new HomeController1();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController1 controller = new HomeController1();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController1 controller = new HomeController1();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        // GET: NewsTest
        List<NewsTest> news = new List<NewsTest>();

        public HomeControllerTest() { }

        public HomeControllerTest(List<NewsTest> products)
        {
            this.news = products;
        }

        public IEnumerable<NewsTest> GetAllNews()
        {
            return news;
        }

        public async Task<IEnumerable<NewsTest>> GetAllNewsAsync()
        {
            return await Task.FromResult(GetAllNews());
        }

        public IHttpActionResult GetNews(int id)
        {
            var product = news.FirstOrDefault((p) => p.id == id);
            if (product == null)
            {
                return HttpNotFoundResult();
            }
            return Ok(product);
        }

        private IHttpActionResult Ok(NewsTest product)
        {
            throw new NotImplementedException();
        }

        private IHttpActionResult HttpNotFoundResult()
        {
            throw new NotImplementedException();
        }

        public async Task<IHttpActionResult> GetNewsAsync(int id)
        {
            return await Task.FromResult(GetNews(id));
        }
    }
}
