using Microsoft.VisualStudio.TestTools.UnitTesting;
//using StudentOrganization.Controllers;
//using StudentOrganization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Tests
{
    public class Test
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

            var result = controller.GetNews(4) as OkNegotiatedContentResult<NewsTest>;
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
            testProducts.Add(new NewsTest { id = 1, title = "Demo1", short_description = "short",description="description" });
            testProducts.Add(new NewsTest { id = 2, title = "Demo1", short_description = "short", description = "description" });
            testProducts.Add(new NewsTest { id = 3, title = "Demo1", short_description = "short", description = "description" });
            testProducts.Add(new NewsTest { id = 4, title = "Demo1", short_description = "short", description = "description" });

            return testProducts;
        }


        // Admin Test


        [TestMethod]
        public void GetAllGroups_ShouldReturnAllGroups()
        {
            var testProducts = GetTestGroups();
            var controller = new AdminTestController(testProducts);

            var result = controller.GetAllGroup() as List<StudentOrganization.Models.NewsTest>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllGroupsAsync_ShouldReturnAllGroups()
        {
            var testProducts = GetTestGroups();
            var controller = new AdminTestController(testProducts);

            var result = await controller.GetAllGroupAsync() as List<NewsTest>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        [TestMethod]
        public void GetGroup_ShouldReturnCorrectGroup()
        {
            var testProducts = GetTestGroups();
            var controller = new AdminTestController(testProducts);

            var result = controller.GetGroup(4) as OkNegotiatedContentResult<NewsTest>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts[3].group_name, result.Content.title);
        }

        [TestMethod]
        public async Task GetGroupAsync_ShouldReturnCorrectGroup()
        {
            var testProducts = GetTestGroups();
            var controller = new AdminTestController(testProducts);

            var result = await controller.GetGroupAsync(4) as OkNegotiatedContentResult<NewsTest>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts[3].group_name, result.Content.title);
        }

        [TestMethod]
        public void GetGroups_ShouldNotFindGroups()
        {
            var controller = new AdminTestController(GetTestGroups());

            var result = controller.GetGroup(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private List<Group> GetTestGroups()
        {
            var testGroups = new List<Group>();
            testGroups.Add(new Group { id = 1, group_name = "Demo1", courseId = 1 });
            testGroups.Add(new Group { id = 2, group_name = "Demo1", courseId = 2 });
            testGroups.Add(new Group { id = 3, group_name = "Demo1", courseId = 3 });
            testGroups.Add(new Group { id = 4, group_name = "Demo1", courseId = 4 });

            return testGroups;
        }
    }
}
