using StudentOrganization.Models;
using StudentOrganization.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentOrganization.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext context;

        public UserController()
        {
            context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }
        
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrationPage()
        {
            var groups = context.Groups.ToList();
            var st = context.Students.ToList();
            var student = new ViewStudentModel
            {
                group = groups
            };
            bindState();
            return View();
        }

        public void bindState()
        {
            var course = context.Courses.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select Course", Value = "0" });
            foreach(var c in course)
            {
                li.Add(new SelectListItem { Text = c.number_course.ToString(), Value = c.id.ToString() });
                ViewBag.Course = li;
            }
        }
        
        public JsonResult getGroup(long id)
        {
            var group = context.Groups.Where(x => x.courseId == id).ToList();
            List<SelectListItem> ligroup = new List<SelectListItem>();
            ligroup.Add(new SelectListItem { Text = "Select Course", Value = "0" });
            if (group != null)
            {
                foreach(var g in group)
                {
                    ligroup.Add(new SelectListItem { Text = g.group_name, Value = g.id.ToString() });
                }
            }
            return Json(new SelectList(ligroup, "Value", "Text", JsonRequestBehavior.AllowGet));
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Student student,string re_pass,Group g,long Group)
        {
            
            if (student.password.Equals(re_pass))
            {
                var gr = await context.Groups.FindAsync(Group);
                student.group = gr;
                context.Students.Add(student);
                context.SaveChanges();
                Session["current_user"] = student;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("RegistrationPage","User");
            }
        }

        //public ActionResult Details(int id)
        //{
        //    var users =  context.Students.SingleOrDefault(u => u.id == id);
        //    if (users == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(users);
        //}
    }
}