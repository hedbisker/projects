using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcGradesAngularjs
{
    public class Course : IComparable
    {
        public string Username { set; get; }  // the name field 
        public string Name { set; get; }  // the name field 
        public int Grade { set; get; }
        public double Points { set; get; }  // the Name property
        public bool Exempt{set;get;}
        public int Semester { get; set; }

        public Course(string username, string name, int grade, double points, int semester, bool exempt=false){
            Username = username;
            Name = name;
            Grade = grade;
            Points = points;
            Semester = semester;
            Exempt = exempt;
        }

        public Course(Course c) : this(c.Username,c.Name, c.Grade, c.Points, c.Semester, c.Exempt) { }

        public Course() : this("","", 0, 0, 0, false){}
    
        public override string ToString()
        {
            return Username + '\t' + Name + '\t' + Grade + '\t' + Points + '\t' + Semester + '\t' + Exempt;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return (Username == ((Course)obj).Username
                && Name == ((Course)obj).Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            Course stu = obj as Course;
            int n = 0;
            if (stu != null)
               n = Name.CompareTo(stu.Name);
            return (n != 0) ? n : Username.CompareTo(stu.Username);
        }
    }
}