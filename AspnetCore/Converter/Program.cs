
using Converter.Application.Services;
using Converter.Contracts.Interfaces;

namespace Converter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IConverterService, ConverterService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var allowSpecificOrigins = "converterPage";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: allowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200");
                                      policy.WithMethods("GET");
                                  });
            });

            var app = builder.Build();

            app.UseCors(allowSpecificOrigins);

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