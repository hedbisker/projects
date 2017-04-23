using MvcGradesAngularjs;
using MyGradesAvgProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading;

namespace MyGradesAvgProject.Controllers
{
    public class CourseController : Controller
    {
        private ICourseDbManager courseDbManager;
        static readonly object key = new object();

        private ICourseDbManager getDBManager()
        {
            return CourseDbLinqToSqlManager.getInstance();
        }
        private void addCourse(Course course)
        {
            courseDbManager = getDBManager();
            course.Username = Request.Cookies.Get("username").Value;
            courseDbManager.addCourse(course, course.Username);
        }

        private bool courseIsExist(string username, string name)
        {
            courseDbManager = getDBManager();
            return courseDbManager.courseExist(username, name);
        }


        [HttpPost]
        public int addCourseByUserAndPass(String Username,string name, string grade, string points, int semester, string exempt)
        {
            HttpCookie hc = new HttpCookie("username", Username);
            hc.Expires = DateTime.Now.AddYears(1);
            Response.SetCookie(hc);
            return addCourse(name, grade, points, semester, exempt);
        }

        [HttpPost]
        public int addCourse(string name, string grade, string points, int semester, string exempt)
        {
            exempt = (exempt == null) ? "false" : exempt;
            int myGrade = Convert.ToInt32(grade);
            double myPoints = Convert.ToDouble(points);
            int mySemester = Convert.ToInt32(semester);
            bool myExempt = Convert.ToBoolean(exempt);
            lock (key)
            {
                if (courseIsExist(Request.Cookies.Get("username").Value, name))
                    return -1;
                addCourse(new Course(Convert.ToString(Request.Cookies.Get("username").Value), name, myGrade, myPoints, mySemester, myExempt));
                return 1;
            }
        }

        [HttpPost]
        public object fetchCoursesByUserAndPass(string username,string password)
        {
            lock (key)
            {
                object json = new JavaScriptSerializer().Serialize(new Course[0] { });
                IStudentDbManager istudentDBMng = StudentLinqToSqlDbManager.getInstance();
                if (istudentDBMng.accountExist(username,password))
                {
                    Course[] courses = fetchCourses(username);
                    json = new JavaScriptSerializer().Serialize(courses);
                    if (courses == null)
                        return null;
                }
                return json;
            }

        }

        [HttpPost]
        public object fetchCourses()
        {
            lock (key)
            {
                object json = new JavaScriptSerializer().Serialize(new Course[0]{});
                if(HttpContext.Request.Cookies["username"] != null){
                    string username = Convert.ToString(HttpContext.Request.Cookies["username"].Value);
                    Course[] courses = fetchCourses(username);
                    json = new JavaScriptSerializer().Serialize(courses);
                    if (courses == null)
                        return null;
                }
                return json;
            }
                
        }

        private Course[] fetchCourses(string username)
        {
            courseDbManager = getDBManager();
            return courseDbManager.getCourses(username.ToString().Trim());
        }

        [HttpPost]
        public int deleteCourseByUsername(String username,string courseName)
        {
            lock (key)
            {
                courseDbManager = getDBManager();
                return courseDbManager.deleteCourse(username, courseName);
            }
        }

        [HttpPost]
        public int deleteCourse(string name)
        {
            lock (key)
            {
                courseDbManager = getDBManager();
                if (HttpContext.Request.Cookies["username"] != null)
                {
                    string username = Convert.ToString(HttpContext.Request.Cookies["username"].Value);
                    return courseDbManager.deleteCourse(username, name);
                }
                return 0;
            }
        }

        [HttpPost]
        public string getAverage()
        {
            lock (key)
            {
                if (HttpContext.Request.Cookies["username"] != null)
                {
                    Course[] courses = fetchCourses(Convert.ToString(HttpContext.Request.Cookies["username"].Value));
                    if (courses == null)
                        return null;
                    var notExemptCousers = courses.Where(course => !course.Exempt);
                    double grades = (from course in notExemptCousers select course.Points * course.Grade).Sum();
                    double points = (from course in notExemptCousers select course.Points).Sum();
                    double fullPoints = (from course in courses select course.Points).Sum();
                    JsonResult js = Json(new
                    {
                        points = Convert.ToString(fullPoints),
                        average = (grades / points).ToString("##.##")
                    });
                    return JsonConvert.SerializeObject(js.Data);
                }
                return JsonConvert.SerializeObject(Json(new { points = "---", average = "---" }).Data);
            }
        }

        [HttpPost]
        public int overrideCourseByUsername(String username,string oldName, string name, string grade, string points, string semester, string exempt)
        {
            HttpCookie hc = new HttpCookie("username", username);
            hc.Expires = DateTime.Now.AddYears(1);
            Response.SetCookie(hc);
            return overrideCourse(oldName, name, grade, points, semester, exempt);
        }

        [HttpPost]
        public int overrideCourse(string oldName, string name, string grade, string points, string semester,string exempt)
        {
            lock (key)
            {
                courseDbManager = getDBManager();
                string username = Request.Cookies.Get("username").Value;
                return courseDbManager.updateCourse(oldName, username, name, grade, points, semester, exempt);
            }
        }
    }
}

