using MvcGradesAngularjs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGradesAvgProject.Models
{
    public interface ICourseDbManager
    {
        void addCourse(Course course, string username);
        bool courseExist(string username, string name);
        Course[] getCourses(string username);
        int deleteCourse(string username, string name);
        int updateCourse(string oldName, string username, string name, string grade, string points, string semester, string exempt);
    }
}
