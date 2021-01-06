using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using StudentOrganization.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentOrganization.Controllers
{
    public class OrganizationController : Controller
    {
        protected ApplicationDbContext db { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        public OrganizationController()
        {
            db = new ApplicationDbContext();
            //this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        public ActionResult SendRequest(long id)
        {
            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string userID = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(x => x.Id == userID);
            var org = db.Organizations.Find(id);
            RequestOrganization req = new RequestOrganization();
            req.orgID = id;
            req.org = org;
            req.studentID = user.Id;
            req.student = user;
            db.RequestOrganizations.Add(req);
            db.SaveChanges();
            return RedirectToAction("DetailsOrganization", "Home",new { id });
        }

        public ActionResult UserOrganization(string Id)
        {
            var organization = db.Organizations.Where(u=>u.leader_id==Id);          
            ViewBag.Organization = organization.ToList();
            ViewBag.Students = db.Users.ToList();
            return View();
        }

        public ActionResult Requests(string Id)
        {
            var organization = db.Organizations.Where(u => u.leader_id == Id).ToList();

            long org_id = 0;
            for(int i = 0; i < organization.Count; i++)
            {
                org_id = organization.ElementAt(i).id;
            }
            var requests = db.RequestOrganizations.Where(r => r.orgID == org_id);
            ViewBag.Requests = requests.ToList();
            ViewBag.CountRequests = requests.ToList().Count;
            ViewBag.Organization = organization;
            ViewBag.Students = db.Users.ToList();
            ViewBag.Groups = db.Groups.ToList();
            return View();
        }

        public ActionResult HandleRequests(long request_id,string btn,string student_id,long org_id)
        {
            string userID = User.Identity.GetUserId();
            var leader = db.Users.FirstOrDefault(x => x.Id == userID);
            if (btn.Equals("confirm"))
            {
                var student = db.Users.Find(student_id);
                var organization = db.Organizations.Find(org_id);
                student.organization_id = org_id;
                //student.organizations.Add(organization);
                //organization.students.Add(student);
                db.SaveChanges();
                var req = db.RequestOrganizations.Find(request_id);
                db.RequestOrganizations.Remove(req);
                db.SaveChanges();
            }
            else if (btn.Equals("reject"))
            {
                var req = db.RequestOrganizations.Find(request_id);
                db.RequestOrganizations.Remove(req);
                db.SaveChanges();
            }
            return RedirectToAction("UserOrganization", "Organization", new { userID });
        }

        public ActionResult CanselRequest(long id)
        {
            var organization = db.Organizations.Find(id);
            string userID = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(x => x.Id == userID);
            var requests = db.RequestOrganizations.ToList();
            long req_id = 0;
            foreach(var r in requests)
            {
                if(r.studentID==userID && r.orgID == id)
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
            foreach(var o in organization)
            {
                org_id = o.id;
            }
            return org_id;
        }

        public bool isInRequest(string Id,long id)
        {
            bool req = false;
            var request = db.RequestOrganizations.ToList();
            foreach(var r in request)
            {
                if(r.orgID==id && r.studentID == Id)
                {
                    req = true;
                }
            }
            return req;
        }

        public void bindStudent()
        {
            var course = db.Users.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select Student", Value = "0" });
            foreach (var c in course)
            {
                li.Add(new SelectListItem { Text = c.Name.ToString() + " "+c.Surname.ToString(), Value = c.Id.ToString() });
                ViewBag.Students = li;
            }
        }

        public ActionResult KickAll()
        {
            string userID = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(x => x.Id == userID);
            var organization = db.Organizations.Where(u => u.leader_id == userID).ToList();
            long org_id = 0;
            foreach(var o in organization)
            {
                org_id = o.id;
            }
            var students = db.Users.Where(x => x.organization_id == org_id).ToList();
            foreach (var s in students)
            {
                s.organization_id = 0;
                s.organizations.Remove(db.Organizations.Find(org_id));
                s.organizations = null;
            }
            db.SaveChanges();
            return RedirectToAction("UserOrganization", "Organization", new { userID });
        }

        public ActionResult KickMember(string student_id)
        {
            string userID = User.Identity.GetUserId();
            var organization = db.Organizations.Where(x => x.leader_id == userID).ToList();
            long org_id = 0;
            foreach(var o in organization)
            {
                org_id = o.id;
            }
            var org = db.Organizations.Find(org_id);
            var student = db.Users.Find(student_id);
            //org.students.Remove(student);
            student.organizations.Remove(org);
            student.organization_id = 0;
            student.organizations = null;            
            db.SaveChanges();
            return RedirectToAction("UserOrganization", "Organization", new { userID });
        }

        public ActionResult HandleRequestsAll(long org_id,string btn)
        {
            string userID = User.Identity.GetUserId();
            var request = db.RequestOrganizations.Where(r => r.orgID == org_id).ToList();
            var organization = db.Organizations.Find(org_id);
            List<string> student_ids = new List<string>();
            foreach(var r in request)
            {
                student_ids.Add(r.studentID);
            }
            List<ApplicationUser> students = new List<ApplicationUser>();

            if (btn.Equals("confirmAll"))
            {
                foreach (var s in student_ids)
                {
                    students.Add(db.Users.Find(s));                    
                }
                foreach (var st in students)
                {
                    st.organization_id = org_id;
                    //st.organizations.Add(organization);
                }
                db.SaveChanges();
                foreach (var r in request)
                {
                    db.RequestOrganizations.Remove(r);
                }
                db.SaveChanges();
            }
            else
            {
                foreach(var r in request)
                {
                    db.RequestOrganizations.Remove(r);
                }
                db.SaveChanges();
            }
            return RedirectToAction("UserOrganization", "Organization", new { userID });
        }

        public ActionResult FindMember(string Id)
        {
            var user = db.Users.Find(Id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New_Organization(string name,string description,string picture_url)
        {
            string userID = User.Identity.GetUserId();
            Organization organization = new Organization();
            organization.name = name;
            organization.description = description;
            organization.picture_url = picture_url;
            organization.leader_id = userID;
            db.Organizations.Add(organization);
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult UpdateFullName(string name,string surname)
        {
            string userID = User.Identity.GetUserId();
            var user = db.Users.Find(userID);
            user.Name = name;
            user.Surname = surname;
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult UpdatePicture(string picture_url)
        {
            string userID = User.Identity.GetUserId();
            var user = db.Users.Find(userID);
            user.picture_url = picture_url;
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }
    }
}