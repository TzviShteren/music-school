using static MusicSchool2207FA.Service.MusicSchoolService;
using static MusicSchool2207FA.Configuration.MusicSchoolConfiguration;
using MusicSchool2207FA.Model;
namespace MusicSchool2207FA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateXmlIfNotExists();
            //InsertClassroom("Jazz guitar");
            //InsertTeacher("sisu" ,"Jazz guitar");
            //AddStudent("dir", "guitar", "Jazz guitar");
            //AddManyStudent("Jazz guitar", new StudentModel("dfgh", new Instrument ("vghk")), new StudentModel("fvd", new Instrument("tgdee")));
            //UpdateTeacherName("max", "DT max");
        }
    }
}
