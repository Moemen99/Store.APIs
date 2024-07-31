using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Store.APIs.Errors;
using Store.APIs.Extensions;
using Store.APIs.Helpers;
using Store.APIs.Middlewares;
using Store.Core.Repositories.Contract;
using Store.Repository;
using Store.Repository.Data;

namespace Store.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConfigureServices
            // Add services to the container.

            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options/*.UseLazyLoadingProxies()*/.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddApplicationServices();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontBaseUrl"]);
                });
            });
            #endregion

            var app = builder.Build();

            #region Update-Database
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var _dbContext = service.GetRequiredService<StoreContext>();
            // Ask CLR For Creating Object From DbContext Explicitly
            var loggeFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                await CsvProcessor.ProcessCsvAsync(loggeFactory);
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);

            }
            catch (Exception ex)
            {
                var logger = loggeFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during applying the migration");
            }
            #endregion

            #region Configure KestrelMiddlewares
            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();


            app.UseCors("MyPolicy");
            app.UseAuthorization();

                
            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}