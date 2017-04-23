using MvcGradesAngularjs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyGradesAvgProject.Models
{
    public class CourseDbManager : ICourseDbManager
    {
       private readonly string coursesTable = StaticLogicFunctions.coursesTable; 
       private static CourseDbManager courseDbManager = null;
       private static object syncRoot = new Object();
       private CourseDbManager() { }

       public static CourseDbManager getInstance()
       {
           lock (syncRoot)
           {
               if(courseDbManager == null){
                   courseDbManager = new CourseDbManager();
               }
               return courseDbManager;
           }

       }

       public void addCourse(Course course, string username)
       {
           string queryString = "INSERT INTO " + coursesTable +" "+
               "VALUES ('" + username + "','" + course.Name + "','" +  course.Grade + 
               "','" + course.Points + "','" + course.Semester + "','" + course.Exempt + "');";
           SqlConnection connection = new SqlConnection(StaticLogicFunctions.GetConnectionString());
           try
           {
               SqlCommand myCommand = new SqlCommand(queryString, connection);
               connection.Open();
               myCommand.ExecuteNonQuery();
               myCommand.Cancel();

           }
           finally
           {
               connection.Close();
           }
       }

       public bool courseExist(string username, string name)
       {
           string queryString = "SELECT * " +
                        "FROM " + coursesTable + " " +
                        "WHERE Username='" + username + "' AND Name='" + name + "';";
           SqlConnection connection = new SqlConnection(StaticLogicFunctions.GetConnectionString());
           try
           {
               SqlCommand myCommand = new SqlCommand(queryString, connection);
               connection.Open();
               SqlDataReader myReader = myCommand.ExecuteReader();
               bool courseExist = myReader.Read();
               myCommand.Cancel();
               myReader.Close();
               return courseExist;
           }
           finally
           {
               connection.Close();
           }
       }

        public Course[] getCourses(string username)
        {
           List<Course> coursesList = new List<Course>();


            string queryString = "SELECT * " +
             "FROM " + coursesTable + " " +
             "WHERE Username='" + username + "';";
            
           SqlConnection connection = new SqlConnection(StaticLogicFunctions.GetConnectionString());
           try
           {
               SqlCommand myCommand = new SqlCommand(queryString, connection);
               connection.Open();
               SqlDataReader myReader = myCommand.ExecuteReader();
               while (myReader.Read())
               {
                   /*Course c = new Course(myReader.GetString(1).Trim(),
                     myReader.GetInt32(2), myReader.GetDouble(3), myReader.GetBoolean(4));*/
                   Course c = new Course(Convert.ToString(myReader["Username"]),
                       Convert.ToString(myReader["Name"]).Trim(),
                       Convert.ToInt32(myReader["Grade"]),
                       Convert.ToDouble(myReader["Points"]),
                       Convert.ToInt16(myReader["Semester"]),
                       Convert.ToBoolean(myReader["exempt"]));
                   coursesList.Add(c);
               }
               myCommand.Cancel();
               myReader.Close();
           }
           finally
           {
               connection.Close();
           }
           return coursesList.ToArray();
       }

       public int deleteCourse(string username,string name)
       {
            string queryString = "DELETE FROM " + coursesTable + " WHERE Name='" + name + "' AND Username='" + username + "';";
           SqlConnection connection = new SqlConnection(StaticLogicFunctions.GetConnectionString());
           try
           {
               SqlCommand myCommand = new SqlCommand(queryString, connection);
               connection.Open();
               myCommand.ExecuteNonQuery();
               myCommand.Cancel();
               return 1;
           }
           finally
           {
               connection.Close();
           }
       }
       public int updateCourse(string oldName, string username, string name, string grade, string points, string semester,string exempt)
       {
           string queryString = "UPDATE " + coursesTable + " " +
                   "SET Name='" + name + "',Grade='" + grade + "',Points='" + points + "',Semester='" + semester + "' ,Exempt='" + exempt + "' " +
                   "WHERE Username='" + username + "' AND Name='" + oldName + "';";
           SqlConnection connection = new SqlConnection(StaticLogicFunctions.GetConnectionString());
           try
           {
               SqlCommand myCommand = new SqlCommand(queryString, connection);
               connection.Open();
               myCommand.ExecuteNonQuery();
               myCommand.Cancel();
               return 1;
           }
           finally
           {
               connection.Close();
           }
       }
    }
}