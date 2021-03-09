using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI
{
    public class Startup
    {
        //this code pattern provides access to the
        //IConfiguration interface -  which makes possible
        //access to the configuration stored in the 
        //appsettings.json file --WHICH-- means the app
        //can now read the connectionString and pass it
        //to DbContext

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //this registers the CommandContext (in Service Container) and 
            //point it to the connectionString (PostgreSqlConnection) that 
            //resides on the appsettings.json file
            services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection")));

            services.AddControllers();
            #region "Removed Code that registers ICommandAPIRepo
            //Chapter 6: Applying Dependency Injection services collection to 
            //the code uses the services collection 'services' to register the interface (ICommandAPIRepo) 
            //with the class that impliments the interface
            //AddScoped() tells the DI that the service should be created once per client request
            //REMOVED: services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();
            #endregion


            //now that have swapped out the mock repository for the 
            //sql data via DBContext need to register with the services container 
            //as follows:
            services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
