using MusicSchool2207FA.Model;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using static MusicSchool2207FA.Configuration.MusicSchoolConfiguration;

namespace MusicSchool2207FA.Service
{
    internal static class MusicSchoolService
    {
        // צור אלמנט חדש של בית ספר למוזיקה
        public static void CreateXmlIfNotExists()
        {
            if (!File.Exists(musicSchoolPath))
            {
                // create new document (xml)
                XDocument document = new();
                // create an element
                XElement musicSchool = new("music_school");
                // document add element
                document.Add(musicSchool);
                // document save changes to provided path
                document.Save(musicSchoolPath);
            }
        }

        public static void InsertClassroom(string classroomName)
        {
            XDocument docoment = XDocument.Load(musicSchoolPath);
            XElement? musicSchool = docoment.Descendants("music_school")
                .FirstOrDefault();

            if (musicSchool == null)
            {
                return;
            }

            XElement classRoom = new(
                "class_room",
                new XAttribute("name", classroomName)
                );

            musicSchool.Add(classRoom);
            docoment.Save(musicSchoolPath);

        }
        public static void InsertTeacher(string teacherName, string classroomName)
        {
            XDocument docoment = XDocument.Load(musicSchoolPath);
            XElement? musicSchool = docoment.Descendants("class_room")
                .FirstOrDefault(room => room
                .Attribute("name")?.Value == classroomName);

            if (musicSchool == null)
            {
                return;
            }

            XElement teacher = new(
                "teacher",
                new XAttribute("name", teacherName)
                );

            musicSchool.Add(teacher);
            docoment.Save(musicSchoolPath);
        }

        public static void AddStudent(string studentName, string instrument, string classroomName)
        {
            XDocument docoment = XDocument.Load(musicSchoolPath);
            //XElement? classRoom = docoment.Descendants("class_room")
            //    .FirstOrDefault(room => room
            //    .Attribute("name")?.Value == classroomName);
            XElement? classRoom = (
                from room in docoment.Descendants("class_room")
                where room.Attribute("name")?.Value == classroomName
                select room
                ).FirstOrDefault();

            if (classRoom == null)
                return;

            XElement student = new(
                "student",
                new XAttribute("name", studentName),
                new XElement("instruments", instrument)
            );

            classRoom.Add(student);
            docoment.Save(musicSchoolPath);

        }

        public static void AddManyStudent(string classroomName, params StudentModel[] Students)
        {
            XDocument docoment = XDocument.Load(musicSchoolPath);

            XElement? classRoom = (
                from room in docoment.Descendants("class_room")
                where room.Attribute("name")?.Value == classroomName
                select room
                ).FirstOrDefault();

            if (classRoom == null)
                return;


            List<XElement> studentsXElementList =
                Students
                .Select(ConverStudentToElement)
                .ToList();

            classRoom.Add(studentsXElementList);
            docoment.Save(musicSchoolPath);

        }
        private static XElement ConverStudentToElement(StudentModel Student) => new XElement(
            "student",
            new XAttribute("name", Student.Name),
            new XElement("instruments", Student.instrument.Name)
        );

        public static void UpdateStudentInstrument(string studentName, string newInstrument)
        {
            XDocument docoment = XDocument.Load(musicSchoolPath);

            XElement? selectedStudent = (
                from student in docoment.Descendants("student")
                where student.Attribute("name")?.Value == studentName
                select student
                ).FirstOrDefault();


            selectedStudent = selectedStudent?.Descendants("instruments").FirstOrDefault();

            if (selectedStudent == null)
                return;

            selectedStudent.Value = newInstrument;
            
            docoment.Save(musicSchoolPath);
        }
        public static void UpdateTeacherName(string teacherName, string newTeacherName)
        {
            XDocument docoment = XDocument.Load(musicSchoolPath);

            XElement? selectedDocoment = (
                from student in docoment.Descendants("teacher")
                where student.Attribute("name")?.Value == teacherName
                select student
                ).FirstOrDefault();

            if (selectedDocoment == null)
                return;

            selectedDocoment.SetAttributeValue("name", newTeacherName);
            docoment.Save(musicSchoolPath);
        }

        // עדיין לא יודע אם זה עובד
        public static void ReplaceStudent(StudentModel Student, string studentName)
        {
            XDocument docoment = XDocument.Load(musicSchoolPath);

            XElement? selectedStudent = (
                from student in docoment.Descendants("student")
                where student.Attribute("name")?.Value == studentName
                select student
                ).FirstOrDefault();

            if (selectedStudent == null)
                return;
            // ======================================================================================================
            selectedStudent.ReplaceWith(new XElement("student",
                new XAttribute("name", Student.Name),
                new XElement("instruments", Student.instrument.Name)));
            // ======================================================================================================

            docoment.Save(musicSchoolPath);
        }

        //private Func<string, XDocument, XElement?> LogClass = (x, d) => d.Descendants("class_room")
        //.FirstOrDefault(room => room
        //.Attribute("name")?.Value == x);
    }
}
