using MvcGradesAngularjs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyGradesAvgProject.Models
{

    /*

            */
    public class StudentLinqToSqlDbManager : IStudentDbManager
    {
       private readonly string coursesTable = StaticLogicFunctions.coursesTable;
       private static StudentLinqToSqlDbManager studentDbManager = null;
       private static object syncRoot = new Object();

        private StudentLinqToSqlDbManager() {  }

       public static StudentLinqToSqlDbManager getInstance()
       {
           lock (syncRoot)
           {
               if(studentDbManager == null){
                    studentDbManager = new StudentLinqToSqlDbManager();
               }
               return studentDbManager;
           }

       }

        public void registerNewStudent(string user, string pass)
        {
            tblStudent student = new tblStudent
            {
                Username = user,
                Password = pass,
            };
            using (DataGradesLinqtoSqlDataContext dataGradesLinqtoSqlDataContext = new DataGradesLinqtoSqlDataContext())
            {
                dataGradesLinqtoSqlDataContext.tblStudents.InsertOnSubmit(student);
                dataGradesLinqtoSqlDataContext.SubmitChanges();
            }
        }

        public bool accountExist(string user, string pass)
        {
            using (DataGradesLinqtoSqlDataContext dataGradesLinqtoSqlDataContext = new DataGradesLinqtoSqlDataContext())
            {
                var Student = from StudentRow in dataGradesLinqtoSqlDataContext.tblStudents
                              where StudentRow.Username == user &&
                              StudentRow.Password == pass
                              select StudentRow;
                try
                {
                    tblStudent[] ut = Student.ToArray();
                    if (Student.Any())
                    {
                        return true;
                    }
                }catch(System.InvalidOperationException ioe){}
                return false;
            }
        }
    }
}