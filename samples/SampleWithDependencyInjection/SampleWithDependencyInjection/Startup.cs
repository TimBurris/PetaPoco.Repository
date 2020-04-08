using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleWithDependencyInjection
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //get the connection string for our database, we'll need it to register our DatabaseFactory
            string conString = Microsoft
                                   .Extensions
                                   .Configuration
                                   .ConfigurationExtensions
                                   .GetConnectionString(this.Configuration, "SampleDBConnection");

            //register the services needed by PetaPoco.Repository (this is using the DI method, you could also use the "DefaultConfiguration" method which is outlined in another sample)

            //this one is optional, if you aren't using any services to extend yoru crud repo (like last user/date stamper or logger, you could omit this and just force null in your repository implementations)
            services.AddSingleton<PetaPoco.Repository.Abstractions.ICrudRepositoryServiceCollection, PetaPoco.Repository.CrudRepositoryServiceCollection>();
            services.AddSingleton<PetaPoco.Repository.Abstractions.IDatabaseFactory>(new PetaPoco.Repository.DefaultSqlDatabaseFactory(conString));

            //here we are registering our own data repositories
            services.AddTransient<Data.Repositories.Abstractions.ISalesOrderRepository, Data.Repositories.SalesOrderRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
