using System;
using Grades.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Grades
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // create service collection
            IServiceCollection services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);

            // create service provider
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // Get Service and call method
            var service = serviceProvider.GetService<IMenuService>();
            service?.Invoke();
        }
    }
}
