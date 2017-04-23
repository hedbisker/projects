using MvcGradesAngularjs;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace MyGradesAvgProject.Models
{

    /*
   
            */
    public class CourseDbLinqToSqlManager : ICourseDbManager
    {
        private readonly string coursesTable = StaticLogicFunctions.coursesTable;
        private static CourseDbLinqToSqlManager courseDbManager = null;
        private static object syncRoot = new Object();

        private CourseDbLinqToSqlManager() {}

        public static CourseDbLinqToSqlManager getInstance()
        {
            lock (syncRoot)
            {
                if (courseDbManager == null)
                {
                    courseDbManager = new CourseDbLinqToSqlManager();
                }
                return courseDbManager;
            }
        }

        public void addCourse(Course course, string username)
        {

            tblCourse ord = new tblCourse
            {
                Username = username,
                Name = course.Name,
                Grade = course.Grade,
                Points = course.Points,
                Semester = course.Semester,
                Exempt = course.Exempt
            };
            using (DataGradesLinqtoSqlDataContext dataGradesLinqtoSqlDataContext = new DataGradesLinqtoSqlDataContext())
            {
                dataGradesLinqtoSqlDataContext.tblCourses.InsertOnSubmit(ord);
                dataGradesLinqtoSqlDataContext.SubmitChanges();
                dataGradesLinqtoSqlDataContext.Dispose();
            }
        }

        public bool courseExist(string username, string name)
        {
            using (DataGradesLinqtoSqlDataContext dataGradesLinqtoSqlDataContext = new DataGradesLinqtoSqlDataContext())
            {
                IQueryable<tblCourse> course = from courseRow in dataGradesLinqtoSqlDataContext.tblCourses
                         where courseRow.Username == username &&
                         courseRow.Name == name
                         select courseRow;
                tblCourse[] ut = course.ToArray();
                if (course.Any())
                {
                    return true;
                }
                return false;
            }
        }

        public Course[] getCourses(string username)
        {
            List<Course> coursesList = new List<Course>();
            using (DataGradesLinqtoSqlDataContext dataGradesLinqtoSqlDataContext = new DataGradesLinqtoSqlDataContext())
            {
                IQueryable<tblCourse> courses = from courseRow in dataGradesLinqtoSqlDataContext.tblCourses
                              where courseRow.Username == username
                              select courseRow;
                try
                {
                    if (courses.Any())
                    {
                        var courseDataArr = courses.ToArray();
                        for (int i = 0; i < courseDataArr.Length; i++)
                        {
                            var uda = courseDataArr[i];
                            Course cd = new Course(uda.Username.Trim(), uda.Name.Trim(),
                                uda.Grade, uda.Points, uda.Semester, uda.Exempt);
                            coursesList.Add(cd);
                        }
                        return coursesList.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }catch(System.Data.SqlClient.SqlException sqle)
                {
                    return null;
                }
            }
        }

        public int deleteCourse(string username, string name)
        {
            // Query the database for the rows to be deleted.
            using (DataGradesLinqtoSqlDataContext dataGradesLinqtoSqlDataContext = new DataGradesLinqtoSqlDataContext())
            {
                IQueryable<tblCourse> deleteCourse =
                from course in dataGradesLinqtoSqlDataContext.tblCourses
                where course.Username == username &&
                      course.Name == name
                select course;
                foreach (tblCourse course in deleteCourse)
                {
                    IQueryable<tblCourse> courseToDelete  = dataGradesLinqtoSqlDataContext.tblCourses.Where(x =>
                    ((x.Username == course.Username) && (x.Name == course.Name)));
                    tblCourse c = courseToDelete.ToArray()[0];
                    dataGradesLinqtoSqlDataContext.tblCourses.DeleteOnSubmit(c);
                }
                dataGradesLinqtoSqlDataContext.SubmitChanges();
                return 1;
            }
        }

        public int updateCourse(string oldName, string username, string name, string grade, string points, string semester, string exempt)
        {
            using (DataGradesLinqtoSqlDataContext dataGradesLinqtoSqlDataContext = new DataGradesLinqtoSqlDataContext())
            {
                try
                {
                    var instance = dataGradesLinqtoSqlDataContext.tblCourses.FirstOrDefault(course =>
                        (course.Username == username) && (course.Name == oldName));
                    instance.Grade = Convert.ToInt32(grade);
                    instance.Points = Convert.ToDouble(points);
                    instance.Semester = Convert.ToInt32(semester);
                    instance.Exempt = Convert.ToBoolean(exempt);
                    
                    dataGradesLinqtoSqlDataContext.SubmitChanges();
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}