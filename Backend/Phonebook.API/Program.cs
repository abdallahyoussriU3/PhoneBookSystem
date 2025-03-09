
using Microsoft.EntityFrameworkCore;
using Phonebook.Application.Interfaces;
using Phonebook.Application.Services;
using Phonebook.Domain.Interfaces;
using Phonebook.Infrastructure;
using Phonebook.Infrastructure.Data;
using Phonebook.Infrastructure.Repositories;

namespace Phonebook.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //PostgreSQL
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            
            builder.Services.AddScoped<IContactRepository,ContactRepository>();
            builder.Services.AddScoped<IContactService,ContactService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

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

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
