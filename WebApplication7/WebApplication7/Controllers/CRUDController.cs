using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.DB;

namespace WebApplication7.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        [HttpGet]
        public ActionResult ManageStudent()
        {
            OnlineRegistrasionEntities1 db = new OnlineRegistrasionEntities1();
            var students = db.Students.ToList();
            return View(students);
        }

        [HttpPost]
        public ActionResult ManageStudent(Student student)
        {
            var db = new OnlineRegistrasionEntities1();
          
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("ManageStudent");
           
            var students = db.Students.ToList();
            return View(students);
        }

        [HttpGet]
        public ActionResult ManageCourse()
        {
            OnlineRegistrasionEntities1 db = new OnlineRegistrasionEntities1();
            var courses = db.Courses.ToList();
            return View(courses);
        }

        [HttpPost]
        public ActionResult ManageCourse(Cours course)
        {
            var db = new OnlineRegistrasionEntities1();
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("ManageCourse");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new OnlineRegistrasionEntities1();
            var book = (from b in db.Courses
                        where b.Id == id
                        select b).SingleOrDefault();
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Cours book)
        {
            var db = new OnlineRegistrasionEntities1();
    
            var ext = (from b in db.Courses
                       where b.Id == book.Id
                       select b).SingleOrDefault();
           

            db.Entry(ext).CurrentValues.SetValues(book);

            db.SaveChanges();

            return RedirectToAction("ManageCourse");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new OnlineRegistrasionEntities1();
            var book = (from b in db.Courses
                        where b.Id == id
                        select b).SingleOrDefault();
            db.Courses.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
   