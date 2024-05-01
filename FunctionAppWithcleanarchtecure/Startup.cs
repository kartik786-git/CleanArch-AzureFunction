using Application.Extensions;
using FunctionAppWithcleanarchtecure;
using Infrastructure.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionAppWithcleanarchtecure
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException("some strup issue");
            var config = BuildConfiguration(builder.GetContext().ApplicationRootPath);
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(config);
        }
        private IConfiguration BuildConfiguration(string applicationRootPath)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(applicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()

                .Build();



            return configuration;
        }
    }
}
