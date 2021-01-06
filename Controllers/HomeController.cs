using Microsoft.AspNet.Identity;
using StudentOrganization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentOrganization.Controllers
{
   // [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var news = db.News.ToList();
            List<News> n = new List<News>();
            for (int i = 0; i < 3; i++)
            {
                n.Add(news.ElementAt(i));
            }
            ViewBag.News = news;
            ViewBag.Organizations = db.Organizations.ToList();
            var organz = db.Organizations.ToList();
            List<Organization> list = new List<Organization>();
            for(int i = 0; i < 3; i++)
            {
                list.Add(organz.ElementAt(i));
            }
            ViewBag.Org = list.ToList();
            ViewBag.Events = db.Events.ToList();
            var even = db.Events.ToList();
            List<Event> eve = new List<Event>();
            for (int i = 0; i <3; i++)
            {
                eve.Add(even.ElementAt(i));
            }
            ViewBag.Event = eve;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AutocompleteSearch(string term)
        {
            var models = db.News.Where(a => a.title.Contains(term))
                            .Select(a => new { value = a.title })
                            .Distinct();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteSearchOrg(string term)
        {
            var models = db.Organizations.Where(a => a.name.Contains(term))
                            .Select(a => new { value = a.name })
                            .Distinct();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchNews(string search)
        {
            var news = db.News.Where(n => n.title.Contains(search));
            ViewBag.News = news;
            return View();
        }

        public ActionResult AllOrg()
        {
            ViewBag.Organization = db.Organizations.ToList();
            ViewBag.users = db.Users.ToList();
            
            return View();
        }

        public ActionResult AllNews()
        {
            ViewBag.News = db.News.ToList();

            return View();
        }

        public ActionResult AllEvents()
        {
            ViewBag.Events = db.Events.ToList();

            return View();
        }

        public ActionResult SearchOrg(string search)
        {
            var org = db.Organizations.Where(n => n.name.Contains(search));
            ViewBag.users = db.Users.ToList();
            ViewBag.orgs = db.Organizations.ToList();
            

            ViewBag.Org = org;
            return View();
        }

        public ActionResult SearchEvent(string search)
        {
            var @event = db.Events.Where(n => n.name.Contains(search));
            ViewBag.Org = @event;
            return View();
        }

        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        public ActionResult DetailsOrganization(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            organization.student_lead = db.Users.Find(organization.leader_id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            List<Organization> other = new List<Organization>();
            other.AddRange(db.Organizations.ToList());
            other.Remove(organization);
            ViewBag.Organizations = other;
            return View(organization);
        }

        public bool isInRequest(string Id, long id)
        {
            bool req = false;
            var request = db.RequestOrganizations.ToList();
            foreach (var r in request)
            {
                if (r.orgID == id && r.studentID == Id)
                {
                    return true;
                }
            }
            return false;
        }

        public ActionResult CanselRequest(long id)
        {
            var organization = db.Organizations.Find(id);
            string userID = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(x => x.Id == userID);
            var requests = db.RequestOrganizations.ToList();
            long req_id = 0;
            foreach (var r in requests)
            {
                if (r.studentID == userID && r.orgID == id)
                {
                    req_id = r.id;
                }
            }
            var req = db.RequestOrganizations.Find(req_id);
            db.RequestOrganizations.Remove(req);
            db.SaveChanges();
            return RedirectToAction("DetailsOrganization", "Home", new { id });
        }

        public long getCurrentOrgByLeaderId(string Id)
        {
            var organization = db.Organizations.Where(u => u.leader_id == Id).ToList();
            long org_id = 0;
            foreach (var o in organization)
            {
                org_id = o.id;
            }
            return org_id;
        }

        public bool IsLeader(string Id)
        {
            bool b = false;
            var org = db.Organizations.ToList();
            foreach(var o in org)
            {
                if (o.leader_id.Equals(Id))
                {
                    b = true;
                }
            }
            return b;
        }
    }
}