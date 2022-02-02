using System;
using Grades.Dao;
using Grades.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Grades
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // register services
            // https://www.c-sharpcorner.com/article/understanding-addtransient-vs-addscoped-vs-addsingleton-in-asp-net-core/
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddFile("app.log");
            });
            services.AddSingleton<IMenuService, MenuService>();
            services.AddTransient<IGradesDao, GradesDao>();

            RegisterExceptionHandler();
        }

        public static void RegisterExceptionHandler()
        {
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                AnsiConsole.WriteException(eventArgs.Exception,
                    ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenTypes |
                    ExceptionFormats.ShortenMethods | ExceptionFormats.ShowLinks);
            };
        }
    }
}
