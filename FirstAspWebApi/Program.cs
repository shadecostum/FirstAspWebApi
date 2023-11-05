using FirstAspWebApi.Data;
using FirstAspWebApi.Repositary;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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
            builder.Services.AddControllers();

            //to avoid json cycle added check 2 date nov
            builder.Services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddCors(option=>
            {
                option.AddPolicy("AllowLocalhost4200", builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });//for connecting frond end


            builder.Services.AddTransient(typeof(IRepository<>), typeof(EntityRepository<>));//note
            builder.Services.AddTransient<IUserService, UserService>();//new generatic used registering services
            builder.Services.AddTransient<IContactService, ContactService>();
            builder.Services.AddTransient<IDetailsService, DetailService>();

           // builder.Services.AddTransient<IUserRepo,UserRepo>();//old methodregistering needed must added or doesnt work
           //builder.Services.AddTransient<IContactRepo,ContactRepo>();//register Contact table
            //builder.Services.AddTransient<IDetailsRepo,Detailsrepo>();//register Detailrepo

           
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

            app.UseCors("AllowLocalhost4200");//front end connection

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}