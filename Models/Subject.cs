using System . Data . SqlClient;
using Dapper;

namespace API_Grading_System . Models
    {
    public class Subject : Grade
        {
        private int Id { get; set; }
        public int MaximumDegree { get; set; }
        public int StudentDegree { get; set; }
        public string Grade { get; set; }
        public string Name { get; set; }

        public static async Task<List<Subject>> GetStudentSubjects ( int Id )
            {
            using ( SqlConnection connection = new ( Helper . ConnectionString () ) )
                {
                var Subjects = await connection . QueryAsync<Subject,Grade,Subject > ( $"Exec dbo.GetStudentsSubjects @Id",
                    map:(Subject,Grade) =>
                    {
                        Subject . AcademicYear = Grade . AcademicYear;
                        Subject . Semester1 = Grade . Semester1;
                        Subject . Semester2 = Grade . Semester2;
                        Subject . StudentDegree = (Grade . Semester1 + Grade . Semester2);
                        Subject . Grade = CalculateSubjectGrade ( Subject );
                        return Subject;
                    },
                    splitOn:"AcademicYear",
                    param: new { Id}
                    );
                return Subjects . ToList ();
                }
            }

        }
    }
