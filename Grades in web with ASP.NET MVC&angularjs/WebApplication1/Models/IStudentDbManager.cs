using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGradesAvgProject.Models
{
    public interface IStudentDbManager
    {
        void registerNewStudent(string user, string pass);
        
        /****
         * return true if account exist; else return false
         **/
        bool accountExist(string user,string pass);
    }
}
