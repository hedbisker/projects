using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyGradesAvgProject.Models
{
    public class StaticLogicFunctions  
    {
        public static string GetConnectionString()
        {
            string path_way = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string[] p = path_way.Split('\\');
            path_way = "";
            for (int i = 1; i < p.Length - 1; i++)
                path_way += p[i] + '\\';
            return @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" +
                        path_way + "App_Data\\GradesBase.mdf;Integrated Security=True;";
        }

        public static string GetConnectionStringForUnitTest()
        {
            string path_way = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string[] p = path_way.Split('\\');
            path_way = "";
            for (int i = 1; i < p.Length - 1; i++)
                path_way += p[i] + '\\';
            return "Data Source=(LocalDB)\v11.0;AttachDbFilename=" +
                        path_way + "App_Data\\GradesBase.mdf;Integrated Security=True";
        }
        public readonly static string coursesTable = "tblCourses";
        public readonly static string studentsTable = "tblStudents";
    }
}
