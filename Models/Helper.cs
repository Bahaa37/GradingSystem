namespace API_Grading_System . Models
    {
    public class Helper
        {
        public static string ConnectionString ()
            {
            var builder = WebApplication . CreateBuilder ();
            string Connection = builder . Configuration . GetValue<string> ( "ConnectionStrings:GradingSystem" );
            return Connection;
            }
        }
    }
