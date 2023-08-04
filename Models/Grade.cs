using System . ComponentModel . DataAnnotations;
using System . Data . SqlClient;
using Dapper;

namespace API_Grading_System . Models
    {
    public class Grade
        {
        [Required]
        private int SubjectId { get; set; }
        [Required]
        private int StudentId { get; set; }
        [Required]
        public int Semester1 { get; set; }
        [Required]
        public int Semester2 { get; set; }
        [Required]
        public string AcademicYear { get; set; }

        public static async Task UpdateStudententGrade ( int Id , int SubjectId , int? Semester1 , int? Semester2 )
            {
            using ( SqlConnection connection = new ( Helper . ConnectionString () ) )
                {
                await connection . ExecuteAsync ( "Exec dbo.EditStudentGrades @Id,@SubjectId, @Semester1, @Semester2 " ,
                    new
                        {
                        Id
                        ,
                        SubjectId
                        ,
                        Semester1
                        ,
                        Semester2

                        } );
                }
            }
        static string GradeLiteral ( double SubjectGrade )
            {
            return SubjectGrade switch
                {
                    >= 90 => "A",
                    >= 75 => "B",
                    >= 65 => "C",
                    >= 50 => "D",
                    < 50 => "F",
                    _ => "Has No Grade Yet",
                    };
            }
        public static string CalculateSubjectGrade ( Subject subject )
            {
            double StudentDegree = subject . StudentDegree;
            double SubjectMaxDegree = subject . MaximumDegree;
            double SubjectGrade = StudentDegree / SubjectMaxDegree * 100;
            return GradeLiteral ( SubjectGrade );
            }
        public static string CalulateOverAllGrade ( List<Subject> subjects )
            {
            double AllSubjectMaxGrades = 0;
            double AllStudentMaxGrades = 0;
            foreach ( Subject subject in subjects )
                {
                AllSubjectMaxGrades += subject . MaximumDegree;
                AllStudentMaxGrades += subject . StudentDegree;
                }
            double OverAllGrade = AllStudentMaxGrades / AllSubjectMaxGrades * 100;
            return GradeLiteral ( OverAllGrade );
            }
        }
    }
