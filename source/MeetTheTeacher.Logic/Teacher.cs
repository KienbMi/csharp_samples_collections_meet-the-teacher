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
    public class Teacher : IComparable<Teacher>
    {
        private string _weekday;
        private string _time;
        private string _lessonNr;
        private string _roomNr;

        public string Name { get; set; }

        public Teacher(string fullname, string weekday, string time, string lessonNr, string roomNr)
        {
            Name = fullname;
            _weekday = weekday;
            _time = time;
            _lessonNr = lessonNr;
            _roomNr = roomNr;
        }

        public virtual string GetHtmlForName()
        {
            return $"{WebUtility.HtmlEncode(Name)}";
        }

        public string GetTeacherHtmlRow()
        {   
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<tr>");
            sb.AppendLine($"<td align=\"left\">{GetHtmlForName()}</td>");
            sb.AppendLine($"<td align=\"left\">{_weekday}</td>");
            sb.AppendLine($"<td align=\"left\">{_time}</td>");
            sb.AppendLine($"<td align=\"left\">{_roomNr}</td>");
            sb.AppendLine($"</tr>");

            return sb.ToString();
        }

        public int CompareTo(Teacher other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}
