using PracticeEntityProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PracticeEntityProject.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        PracticeDBEntities dbContext = new PracticeDBEntities(); 
        public ActionResult Index()
        {           
            return View();
        }
        public ActionResult StudentDetails(string text)
        {
            List<Student> StudentList = new List<Student>();
            if (text == null)
            {
                StudentList = dbContext.Students.ToList();
            }
            else
            {
                StudentList = dbContext.Students.Where(m => m.Name.Contains(text)).ToList();
            }
            return PartialView("StudentDetails", StudentList);
        }
        public ActionResult Create()
        {
            ViewBag.DepartmentDetails = dbContext.Departments;
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "StudentId,Name,Gender,City,Salary,DepartmentId")] Student st)
        {
            dbContext.Students.Add(st);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? StudentId)
        {
            Student student = dbContext.Students.FirstOrDefault(m=> m.StudentId == StudentId);
            //Department department = dbContext.Departments.FirstOrDefault(m => m.Id == student.DepartmentId);

            ViewBag.DepartmentDetails = dbContext.Departments;
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "StudentId,Name,Gender,City,Salary,DepartmentId")] Student st)
        {
                dbContext.Entry(st).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");          
        }

        public ActionResult Details(int? StudentId)
        {
            Student student = dbContext.Students.FirstOrDefault(m => m.StudentId == StudentId);
            ViewBag.DepartmentName = dbContext.Departments.Where(m => m.Id == student.DepartmentId).FirstOrDefault().Name;
            return View(student);
        }
        public ActionResult Delete(int? StudentId)
        {
            dbContext.Students.Remove(dbContext.Students.Find(StudentId));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}