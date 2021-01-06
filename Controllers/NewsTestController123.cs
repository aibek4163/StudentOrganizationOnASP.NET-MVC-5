using StudentOrganization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace StudentOrganization.Controllers
{
    public class NewsTestController123 : ApiController
    {
        // GET: NewsTest
        List<NewsTest> news = new List<NewsTest>();

        public NewsTestController123() { }

        public NewsTestController123(List<NewsTest> products)
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
                return NotFound();
            }
            return Ok(product);
        }

        public async Task<IHttpActionResult> GetNewsAsync(int id)
        {
            return await Task.FromResult(GetNews(id));
        }
    }

    public class AdminTestController : ApiController
    {
        // GET: NewsTest
        List<Group> group = new List<Group>();

        public AdminTestController() { }

        public AdminTestController(List<Group> groups)
        {
            this.group = groups;
        }

        public IEnumerable<Group> GetAllGroup()
        {
            return group;
        }

        public async Task<IEnumerable<Group>> GetAllGroupAsync()
        {
            return await Task.FromResult(GetAllGroup());
        }

        public IHttpActionResult GetGroup(int id)
        {
            var product = group.FirstOrDefault((p) => p.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public async Task<IHttpActionResult> GetGroupAsync(int id)
        {
            return await Task.FromResult(GetGroup(id));
        }
    }
}