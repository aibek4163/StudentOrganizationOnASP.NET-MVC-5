using StudentOrganization.Models;
using StudentOrganization.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentOrganization.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db;

        public AdminController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Group =  db.Groups.ToList();
            var courses =  db.Courses.ToList();
            var new_course = new ViewGroupModel
            {
                courses = courses
            };
            return View(new_course);
        }

        
        [HttpPost]
        public  ActionResult New_Group(Group group)
        {
            db.Groups.Add(group);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
            //return View(group);
        }

        public JsonResult FindGroup(long id)
        {
            var group = db.Groups.Find(id);
            return Json(group, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit_Group(Group group)
        {
            Console.WriteLine(group.group_name);
            
            
                var gr = db.Groups.Find(group.id);
                gr.group_name = group.group_name;
                db.Groups.AddOrUpdate(gr);
            
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Delete_Group(Group group)
        {
            db.Entry(group).State = System.Data.Entity.EntityState.Deleted;
            //db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        



        public ActionResult News()
        {
            ViewBag.News = db.News.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult New_News(News news)
        {
            db.News.Add(news);
            db.SaveChanges();
            return RedirectToAction("News", "Admin");
        }

        public async Task<ActionResult> Details_Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Details_Edit(News news,string btn)
        {
            if (ModelState.IsValid)
            {
                if (btn.Equals("save"))
                {
                    db.News.AddOrUpdate(news);
                    //db.Entry(news).State = EntityState.Modified;
                     db.SaveChanges();                    
                }
                else if (btn.Equals("delete"))
                {
                    db.Entry(news).State = System.Data.Entity.EntityState.Deleted;
                    //db.Groups.Remove(group);
                    db.SaveChanges();
                }
                return RedirectToAction("News", "Admin");
            }
            return View(news);
        }

        public ActionResult SportClub()
        {
            return View();
        }








        public ActionResult Student()
        {
            var groups = db.Groups.ToList();
            var new_st = new RegisterViewModel
            {
                groups = groups
            };
            IEnumerable<ApplicationUser> students = db.Users.ToList();
            List<ApplicationUser> students_l = students.ToList();      
            ViewBag.Students = students_l;
            ViewBag.Groups = db.Groups.ToList();
            ViewBag.Course = db.Courses.ToList();
            bindState();
            return View(new_st);
        }

        public void bindState()
        {
            var course = db.Courses.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select Course", Value = "0" });
            foreach (var c in course)
            {
                li.Add(new SelectListItem { Text = c.number_course.ToString(), Value = c.id.ToString() });
                ViewBag.Course = li;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult getGroup(long id)
        {
            var group = db.Groups.Where(x => x.courseId == id).ToList();
            List<SelectListItem> ligroup = new List<SelectListItem>();
            ligroup.Add(new SelectListItem { Text = "Select Course", Value = "0" });
            if (group != null)
            {
                foreach (var g in group)
                {
                    ligroup.Add(new SelectListItem { Text = g.group_name, Value = g.id.ToString() });
                }
            }
            return Json(new SelectList(ligroup, "Value", "Text", JsonRequestBehavior.AllowGet));
        }


        [HttpPost]
        public async Task<ActionResult> New_Student(Student student)
        {
            var gr = await db.Groups.FindAsync(student.group_id);
            student.group = gr;
            db.Students.Add(student);
            db.SaveChanges();
            return RedirectToAction("Student", "Admin");
        }

        public JsonResult FindStudent(string id)
        {
            var student = db.Users.Find(id);
            return Json(student, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> Edit_Student(Student student,long group)
        {           
            var s = await db.Students.FindAsync(student.id);
            s.name = student.name;
            s.surname = student.surname;
            s.login = student.login;
            s.group = db.Groups.Find(group);
            db.Students.AddOrUpdate(s);
            db.SaveChanges();
            return RedirectToAction("Student", "Admin");
        }

        public ActionResult Coach()
        {
            ViewBag.Coach = db.Coaches.ToList();
            List<string> sports = new List<string>();
            sports.Add("Volleyball");
            sports.Add("Basketball");
            sports.Add("Football");
            sports.Add("Chess");
            sports.Add("Athletics");
            ViewBag.SportName = sports;
            return View();
        }

        [HttpPost]
        public ActionResult RegisterCoach(Coach coach)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Coach", "Admin");
            }
            return View();
        }


        public JsonResult FindCoach(long id)
        {
            var coach = db.Coaches.Find(id);
            return Json(coach, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Organization()
        {
            ViewBag.Organizations = db.Organizations.ToList();
            ViewBag.Leaders = db.Users.ToList();
            bindOrg();
            return View();
        }


        public void bindOrg()
        {
            var course = db.Users.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select Course", Value = "0" });
            foreach (var c in course)
            {
                li.Add(new SelectListItem { Text = c.Name.ToString() +" "+ c.Surname.ToString(), Value = c.Id.ToString() });
                ViewBag.Students = li;
            }
        }

        public ActionResult New_Organization(Organization organization)
        {
            var student = db.Users.Find(organization.leader_id);
            organization.student_lead = student;
            
            db.Organizations.Add(organization);
            db.SaveChanges();
            return RedirectToAction("Organization", "Admin");
        }




        //public void bindState()
        //{
        //    List<string> sports = new List<string>();
        //    sports.Add("Volleyball");
        //    sports.Add("Basketball");
        //    sports.Add("Football");
        //    sports.Add("Chess");
        //    sports.Add("Athletics");
        //    List<SelectListItem> li = new List<SelectListItem>();
        //    li.Add(new SelectListItem { Text = "Select Course", Value = "0" });
        //    foreach (var c in sports)
        //    {
        //        li.Add(new SelectListItem { Text = c., Value = c.id.ToString() });
        //        ViewBag.Course = li;
        //    }
        //}
    }
}