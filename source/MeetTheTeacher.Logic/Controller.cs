using System;
using System.Collections.Generic;
using System.Text;

namespace MeetTheTeacher.Logic
{
    /// <summary>
    /// Verwaltung der Lehrer (mit und ohne Detailinfos)
    /// </summary>
    public class Controller
    {
        private readonly List<Teacher> _teachers = new List<Teacher>();
        private readonly Dictionary<string, int> _details = new Dictionary<string, int>();

        /// <summary>
        /// Liste für Sprechstunden und Dictionary für Detailseiten anlegen
        /// </summary>
        public Controller(string[] teacherLines, string[] detailsLines)
        {
            InitDetails(detailsLines);
            InitTeachers(teacherLines);
        }

        public int Count => _teachers.Count;

        public int CountTeachersWithoutDetails => _teachers.Count - CountTeachersWithDetails;

        /// <summary>
        /// Anzahl der Lehrer mit Detailinfos in der Liste
        /// </summary>
        public int CountTeachersWithDetails
        {
            get
            {
                int counter = 0;

                foreach (var teacher in _teachers)
                {
                    if (teacher is TeacherWithDetail)
                        counter++;
                }
                return counter;
            }
        }

        /// <summary>
        /// Aus dem Text der Sprechstundendatei werden alle Lehrersprechstunden 
        /// eingelesen. Dabei wird für Lehrer, die eine Detailseite haben
        /// ein TeacherWithDetails-Objekt und für andere Lehrer ein Teacher-Objekt angelegt.
        /// </summary>
        /// <returns>Anzahl der eingelesenen Lehrer</returns>
        private void InitTeachers(string[] lines)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            foreach (var line in lines)
            {
                bool isHeader = line.Contains("Name;Tag;EH;Zeit;Raum;Bemerkung");
                if (isHeader == false)
                {
                    //"BILLINGER Franz; Montag; 4.EH; 10:55 - 11:45 h; 142; ; Dipl.Päd.Dipl.- HTL - Ing.; FOL",
                    string[] data = line.Split(';');

                    if (data != null &&
                        data.Length >= 5)
                    {
                        string fullname = data[0];
                        string weekday = data[1];
                        string lessonNr = data[2];
                        string time = data[3];
                        string roomNr = data[4];
                        int id;

                        if (_details.TryGetValue(fullname.ToLower(), out id))
                        {
                            _teachers.Add(
                                new TeacherWithDetail(
                                    fullname,
                                    weekday,
                                    time,
                                    lessonNr,
                                    roomNr,
                                    id));
                        }
                        else
                        {
                            _teachers.Add(
                                new Teacher(
                                    fullname,
                                    weekday,
                                    time,
                                    lessonNr,
                                    roomNr));
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Lehrer, deren Name in der Datei IgnoredTeachers steht werden aus der Liste 
        /// entfernt
        /// </summary>
        public void DeleteIgnoredTeachers(string[] names)
        {
            if (names == null)
                throw new ArgumentNullException(nameof(names));

            foreach (var name in names)
            {
                int index = FindIndexForTeacher(name);
                if (index >= 0)
                {
                    _teachers.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Sucht Lehrer in Lehrerliste nach dem Namen
        /// </summary>
        /// <param name="teacherName"></param>
        /// <returns>Index oder -1, falls nicht gefunden</returns>
        private int FindIndexForTeacher(string teacherName)
        {
            if (teacherName == null)
                throw new ArgumentNullException(nameof(teacherName));

            int index = -1;
            int counter = 0;

            foreach (var teacher in _teachers)
            {
                if (teacher.Name.ToLower().CompareTo(teacherName.ToLower()) == 0)
                {
                    index = counter;
                    break;
                }
                counter++;
            }
            return index;
        }


        /// <summary>
        /// Ids der Detailseiten für Lehrer die eine
        /// derartige Seite haben einlesen.
        /// </summary>
        private void InitDetails(string[] lines)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            foreach (var line in lines)
            {
                //"Billinger Franz; 2219"
                string[] data = line.Split(';');

                string fullname;
                int id;
                if (data != null && 
                    data.Length >= 2 && 
                    int.TryParse(data[1], out id))
                {
                    fullname = data[0].ToLower();
                    _details.Add(fullname, id);
                }
            }
        }

        /// <summary>
        /// HTML-Tabelle der ganzen Lehrer aufbereiten.
        /// </summary>
        /// <returns>Text für die Html-Tabelle</returns>
        public string GetHtmlTable()
        {
            _teachers.Sort();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<table id=\"tabelle\">");
            sb.AppendLine();
            sb.AppendLine("<tr>");
            sb.AppendLine("<th align=\"center\">Name</th>");
            sb.AppendLine("<th align=\"center\">Tag</th>");
            sb.AppendLine("<th align=\"center\">Zeit</th>");
            sb.AppendLine("<th align=\"center\">Raum</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine();
            sb.AppendLine();

            foreach (var teacher in _teachers)
            {
                sb.AppendLine(teacher.GetTeacherHtmlRow());
            }

            sb.AppendLine("</table>");

            return sb.ToString();
        }
    }
}
