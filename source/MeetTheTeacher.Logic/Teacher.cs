using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MeetTheTeacher.Logic
{
    /// <summary>
    /// Verwaltet einen Eintrag in der Sprechstundentabelle
    /// Basisklasse für TeacherWithDetail
    /// </summary>
    public class Teacher
    {
        private string _fullname;
        private string _weekday;
        private string _time;
        private string _lessonNr;
        private string _roomNr;

        public Teacher(string fullname, string weekday, string time, string lessonNr, string roomNr)
        {
            _fullname = fullname;
            _weekday = weekday;
            _time = time;
            _lessonNr = lessonNr;
            _roomNr = roomNr;
        }

        public virtual string GetHtmlForName()
        {
            return $"{_fullname}";
        }
    }
}
