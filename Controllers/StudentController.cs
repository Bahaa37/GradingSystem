using API_Grading_System . Models;
using Microsoft . AspNetCore . Mvc;

namespace API_Grading_System . Controllers
    {
    [Route ( "api/[controller]" )]
    [ApiController]
    public class StudentController : ControllerBase
        {

        [HttpGet ( "GetAll" )]
        public async Task<IActionResult> GetStudents ()
            {
            try
                {
                List<Student> students = await Student . GetStudents ();
                if ( students . Count > 0 )
                    {
                    return Ok ( students );
                    }
                return NoContent ();
                }
            catch ( Exception )
                {
                return BadRequest ();
                }
            }

        [HttpGet ( "GetStudent/{Id}" )]
        public async Task<IActionResult> GetStudent ( int Id )
            {
            try
                {
                Student student = await Student . GetStudentById ( Id );
                if ( student != null )
                    {
                    return Ok ( student );
                    }
                return NotFound ();
                }
            catch ( Exception )
                {
                return BadRequest ();
                }
            }

        [HttpPost ( "AddStudent" )]
        public async Task<IActionResult> AddStudent ( string Name , string NationalId , string AcademicYear )
            {
            try
                {
                await Student . AddStudent ( Name , NationalId , AcademicYear );
                return Ok ();
                }
            catch ( Exception )
                {
                return BadRequest ();
                }

            }

        [HttpPost ( "EditStudent/{Id}" )]
        public async Task<IActionResult> EditStudent ( int Id , string Name , string NationalId , string AcademicYear )
            {
            try
                {
                await Student . EditStudent ( Id , Name , NationalId , AcademicYear );
                return Ok ();
                }
            catch ( Exception )
                {
                return BadRequest ();
                }
            }
        }
    }
