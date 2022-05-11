//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using System.Text.Json;

namespace D20Tek.CountryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure web application.
            Configure(app, app.Environment, app as IEndpointRouteBuilder);

            // Run the application.
            app.Run();
        }

        internal static void ConfigureServices(IServiceCollection services)
        {
            // add telemetry services
            services.AddApplicationInsightsTelemetry();

            // add controllers with defualt Json serialization options.
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        internal static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEndpointRouteBuilder endpointRoutes)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();

            endpointRoutes.MapControllers();
        }
    }
}