using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MeetTheTeacher.Logic
{
    /// <summary>
    /// Klasse, die einen Detaileintrag mit Link auf dem Namen realisiert.
    /// </summary>
    public class TeacherWithDetail : Teacher
    {
        private int _id;

        public TeacherWithDetail(string fullname, string weekday, string time, string lessonNr, string roomNr, int id) : base(fullname, weekday, time, lessonNr, roomNr)
        {
            _id = id;
        }

        public override string GetHtmlForName()
        {
            return $"<a href=\"?id={_id}\">{base.GetHtmlForName()}</a>";
        }
    }
}
