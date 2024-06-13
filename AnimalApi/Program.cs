
using AnimalApi.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;


namespace AnimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AnimalDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 21))));


            // Add services to the container.

            builder.Services.AddFastEndpoints();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnimalManagementAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseFastEndpoints();
            app.MapControllers();

            app.Run();
        }
    }
}
