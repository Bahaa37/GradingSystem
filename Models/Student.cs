using System . ComponentModel . DataAnnotations;
using System . Data . SqlClient;
using Dapper;

namespace API_Grading_System . Models
    {
    public class Student
        {
        private int Id { get; set; }
        [Required]
        [StringLength ( 14 )]
        public string NationalId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AcademicYear { get; set; }
        public string OverAllGrade { get; set; }
        public List<Subject>? Subjects { get; set; }

        private static async Task GetSubjectsAndCalculateGrade ( Student student )
            {
            student . Subjects = await Subject . GetStudentSubjects ( student . Id );
            student . OverAllGrade = Grade . CalulateOverAllGrade ( student . Subjects );
            }

        public static async Task<List<Student>> GetStudents ()
            {
            using ( SqlConnection connection = new ( Helper . ConnectionString () ) )
                {
                IEnumerable<Student> Students = await connection . QueryAsync<Student> ( "select * from student" );
                foreach ( Student student in Students )
                    {
                    await GetSubjectsAndCalculateGrade ( student );
                    }
                return Students . ToList ();
                }
            }
        public static async Task<Student> GetStudentById ( int id )
            {
            using ( SqlConnection connection = new ( Helper . ConnectionString () ) )
                {
                Student student = await connection . QueryFirstOrDefaultAsync<Student> ( $"select * from student where Id ={id}" );
                await GetSubjectsAndCalculateGrade ( student );
                return student;
                }
            }
        public static async Task AddStudent ( string Name , string NationalId , string AcademicYear )
            {
            using ( SqlConnection connection = new ( Helper . ConnectionString () ) )
                {
                await connection . ExecuteAsync ( "Exec dbo.AddStudent @Name, @NationalId, @AcademicYear " ,
                    new
                        {
                        Name
                        ,
                        NationalId
                        ,
                        AcademicYear
                        } );
                }
            }
        public static async Task EditStudent ( int Id , string Name , string NationalId , string AcademicYear )
            {
            using ( SqlConnection connection = new ( Helper . ConnectionString () ) )
                {
                await connection . ExecuteAsync ( "Exec dbo.EditStudent @Id, @Name, @NationalId, @AcademicYear" ,
                    new
                        {
                        Id
                        ,
                        Name
                        ,
                        NationalId
                        ,
                        AcademicYear
                        } );
                }
            }
        public static async Task DeleteStudent ( int Id )
            {
            using ( SqlConnection connection = new ( Helper . ConnectionString () ) )
                {
                await connection . ExecuteAsync ( "Exec DeleteStudent @Id" , new { Id } );
                }
            }
        }
    }
