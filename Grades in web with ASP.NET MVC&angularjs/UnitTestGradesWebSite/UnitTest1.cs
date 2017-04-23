using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGradesAvgProject.Models;
using MvcGradesAngularjs;

namespace UnitTestGradesWebSite
{
    [TestClass]
    public class UnitTest1
    {

        public TestContext TestContext { set; get; }

        
        [TestMethod]
        [DataSource("System.Data.SqlClient",
            @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\hed\Dropbox\עבודות תכנות\ASP.NET,  MVC\Grades in web with ASP.NET MVC&angularjs\WebApplication1\App_Data\GradesBase.mdf;Integrated Security=True",
            "tblCourses", DataAccessMethod.Sequential)]
            public void TestMethod1()
        {
            int grade = Convert.ToInt32(TestContext.DataRow["Grade"].ToString());
            object obj = 1;
            if(grade < 0 || 100 < grade){
                obj = null;
            }
            Assert.IsNotNull(obj);
        }
    }
}
