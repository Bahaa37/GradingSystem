using API_Grading_System . Models;
using Microsoft . AspNetCore . Http;
using Microsoft . AspNetCore . Mvc;

namespace API_Grading_System . Controllers
    {
    [Route ( "api/[controller]" )]
    [ApiController]
    public class GradeController : ControllerBase
        {
        [HttpPost ( "EditGrade/{StudentId}/{SubjectId}" )]
        public async Task<IActionResult> EditStudent ( int StudentId , int SubjectId , int? Semester1 , int? Semester2 )
            {
            try
                {
                await Grade . UpdateStudententGrade ( StudentId , SubjectId , Semester1 , Semester2 );
                return Ok ();
                }
            catch ( Exception )
                {
                return BadRequest ();
                }
            }
        }
    }
