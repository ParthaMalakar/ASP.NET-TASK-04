using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.DB;

namespace WebApplication7.Controllers
{
    public class StudentController : Controller
    {
        // GET: User

        public ActionResult Registration()
        {
            OnlineRegistrasionEntities1 db = new OnlineRegistrasionEntities1();
            var students = db.Students.ToList();

            return View(students);
        }
      
        public ActionResult RegistrationPage(int id)
        {
            OnlineRegistrasionEntities1 db = new OnlineRegistrasionEntities1();
            var student = db.Students.Find(id);
            var courseRecords = student.CourseStudents;

            var allCourses = db.Courses.ToList();
            var availableCourses = db.Courses.ToList();

            if (courseRecords.Count > 0)
            {
                foreach (var c in allCourses)
                {
                    foreach (var sc in courseRecords)
                    {
                        if (c.Id == sc.CourseId)
                        {
                            if (sc.Marks < 60 || sc.Grade == "W")
                            {
                                continue;
                            }
                            availableCourses.Remove(c);
                        }
                    }
                }
            }

            var eligibleCourses = new List<Cours>();

            //foreach (var c in availableCourses)
            //{
            //    foreach (var sc in courseRecords)
            //    {
            //        if (c.PreReq == 0 && !eligibleCourses.Contains(c))
            //        {
            //            eligibleCourses.Add(c);
            //        }
            //        else if (sc.courseId == c.PreReq && !eligibleCourses.Contains(c))
            //        {
            //            eligibleCourses.Add(c);
            //        }
            //    }
            //}


            if (courseRecords.Count == 0)
            {
                foreach (var c in availableCourses)
                {
                    if (c.PreReq == 0 && !eligibleCourses.Contains(c))
                    {
                        eligibleCourses.Add(c);
                    }
                }
            }
            else
            {
                foreach (var sc in courseRecords)
                {
                    foreach (var c in availableCourses)
                    {
                        if (c.PreReq == 0 && !eligibleCourses.Contains(c))
                        {
                            eligibleCourses.Add(c);
                        }
                        else if (sc.CourseId == c.PreReq && !eligibleCourses.Contains(c))
                        {
                            eligibleCourses.Add(c);
                        }
                    }
                }
            }

            TempData["StudentId"] = student.Id;
            return View(eligibleCourses);
        }

        [HttpPost]
        public ActionResult RegistrationConfirm(int[] courses, int studentId)
        {
            OnlineRegistrasionEntities1 db = new OnlineRegistrasionEntities1();
            var courseStudents = db.CourseStudents.ToList();

            foreach (var c in courses)
            {
                CourseStudent cs = new CourseStudent();
                cs.StudentId = studentId;
                cs.CourseId = c;
                cs.Marks = 0;
                cs.Status = "0";
                cs.Grade = "A";



                db.CourseStudents.Add(cs);
                db.SaveChanges();
            }

            return RedirectToAction("Registration");
        }
    }
}