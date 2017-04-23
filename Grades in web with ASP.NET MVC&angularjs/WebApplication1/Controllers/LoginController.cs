using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Net.Http;
using MvcGradesAngularjs;
using System.Web.Script.Serialization;
using MyGradesAvgProject.Models;

namespace MyGradesAvgProject.Controllers
{
    public class LoginController : Controller
    {
        private IStudentDbManager studentDbManager;
        static readonly object key = new object();

        private IStudentDbManager getDBManager(){
            return StudentLinqToSqlDbManager.getInstance();
        }

        public string getUserName()
        {
            lock (key)
            {
                studentDbManager = getDBManager();
                if (Request.Cookies["username"] == null)
                    return "";
                string username = Request.Cookies["username"].Value;
                return username;
            }
        }
        public int RegisterNewAcc(string user, string pass)
        {
            lock (key)
            {
                if (AccIsAlradyExist(user, pass))
                {
                    return -1;
                }
                else
                {
                    Register(user, pass);
                    return 1;
                }
            }
        }

        public void logout()
        {
            lock (key)
            {
                string[] cookies = Request.Cookies.AllKeys;
                foreach (string cookie in cookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }
            }
        }

        private void Register(string user, string pass)
        {
            studentDbManager = getDBManager();
            studentDbManager.registerNewStudent(user, pass);
        }

        [HttpPost]
        public bool AccIsAlradyExist(string user, string pass)
        {
           
                studentDbManager = getDBManager();
                return studentDbManager.accountExist(user, pass);
        }

        [HttpPost]
        public int saveUser(string user,string pass)
        {
            lock (key)
            {
                int ret = 0;
                studentDbManager = getDBManager();
                if (studentDbManager.accountExist(user, pass))
                {
                    HttpCookie hc = new HttpCookie("username", user.ToString());
                    hc.Expires = DateTime.Now.AddYears(1);
                    Response.SetCookie(hc);
                    ret = 1;
                }
                return ret;
            }
        }
    }
}