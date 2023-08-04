namespace API_Grading_System
    {
    public class Program
        {
        public static void Main ( string [] args )
            {
            var builder = WebApplication . CreateBuilder ( args );

            // Add services to the container.
            string policy = "policy";
            builder . Services . AddControllers ();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder . Services . AddEndpointsApiExplorer ();
            builder . Services . AddSwaggerGen ();
            builder . Services . AddCors ( options =>
            {
                options . AddPolicy ( policy ,
                builder =>
                {
                    builder . AllowAnyOrigin ();
                    builder . AllowAnyMethod ();
                    builder . AllowAnyHeader ();
                } );
            } );

            var app = builder . Build ();

            // Configure the HTTP request pipeline.
            if ( app . Environment . IsDevelopment () )
                {
                app . UseSwagger ();
                app . UseSwaggerUI ();
                }

            app . UseHttpsRedirection ();

            app . UseAuthorization ();
            app . UseCors ( policy );


            app . MapControllers ();

            app . Run ();
            }
        }
    }