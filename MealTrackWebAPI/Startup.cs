using MealTrackWebAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MealTrackWebAPI.Services;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace MealTrackWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MealTrackDatabaseSettings>(Configuration.GetSection(nameof(MealTrackDatabaseSettings)));
            services.AddSingleton<IMealTrackDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MealTrackDatabaseSettings>>().Value);
            services.AddSingleton<UserService>();
            services.AddSingleton<FoodService>();
            services.AddSingleton<MealService>();
            services.AddDirectoryBrowser();
            services.AddControllers();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            var cachePeriod = "600";
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Content")),
                RequestPath = "/content",
                OnPrepareResponse = ctx => {
                    ctx.Context.Response.Headers.Append("Cache-Control",$"public, max-age={cachePeriod}");
                }
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Content")),
                RequestPath = "/content"
            });

            app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseRouting();

            app.UseEndpoints(endPoints => endPoints.MapControllers());
        }
    }
}