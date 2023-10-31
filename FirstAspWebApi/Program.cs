using FirstAspWebApi.Data;
using FirstAspWebApi.Repositary;
using Microsoft.EntityFrameworkCore;

namespace FirstAspWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //setting up on  conection MyConetxt
            builder.Services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString"));
            });

           builder.Services.AddTransient<IUserRepo,UserRepo>();//registering needed must added or doesnt work
           builder.Services.AddTransient<IContactRepo,ContactRepo>();//register Contact table
            builder.Services.AddTransient<IDetailsRepo,Detailsrepo>();//register Detailrepo

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}