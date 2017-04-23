using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyGradesAvgProject.Models
{
    public class StudentDbManager : IStudentDbManager
    {
        private readonly string studentsTable = StaticLogicFunctions.studentsTable;
        private static StudentDbManager studentDbManager = null;
        private static object syncRoot = new Object();
        private StudentDbManager() { }

        public static StudentDbManager getInstance()
        {
            lock (syncRoot)
            {
                if (studentDbManager == null)
                {
                    studentDbManager = new StudentDbManager();
                }
                return studentDbManager;
            }
        }

        public void registerNewStudent(string user, string pass)
        {
            string queryString = "INSERT INTO " + studentsTable +" "+
                  "VALUES ('" + user + "','" + pass + "');";
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

        public bool accountExist(string user, string pass)
        {
            string queryString = "SELECT * " +
             "FROM "+studentsTable +" "+
             "WHERE Username='" + user + "';";
            SqlConnection connection = new SqlConnection(StaticLogicFunctions.GetConnectionString());
            try
            {
                SqlCommand myCommand = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                } catch (System.InvalidOperationException ex) {
                    throw ex;
                } catch (System.Data.SqlClient.SqlException ex) {
                    throw ex;
                }catch (System.Configuration.ConfigurationErrorsException ex) {
                    throw ex;
                }
                SqlDataReader myReader = myCommand.ExecuteReader();
                bool userExist = myReader.Read();
                if (userExist)
                {
                    if (Convert.ToString(myReader["Password"]).Trim().Equals(pass) == false)
                    {
                        userExist = false;
                    }
                }
                myCommand.Cancel();
                myReader.Close();
                return userExist;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}