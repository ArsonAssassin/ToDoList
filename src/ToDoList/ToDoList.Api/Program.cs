using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Api.Data;
namespace ToDoList.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(ConfigureServices)
            .Build();

            host.Run();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("TodoConnectionString")));

            services.AddSingleton<ITodoRepository, TodoRepository>();
        }
    }
}
