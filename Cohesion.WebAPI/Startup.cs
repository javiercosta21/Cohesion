using Cohesion.Application.Utils;
using Cohesion.Application.ServiceRequests;
using Cohesion.Infrastructure.InMemoryDataAccess.ServiceRequests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cohesion.WebAPI
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
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            ConfigureIoc(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureIoc(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<Infrastructure.InMemoryDataAccess.DBContext>();
            services.AddTransient<IServiceRequestRepository, ServiceRequestRepository>();
            services.AddTransient<IServiceRequestAppService, ServiceRequestAppService>();
           
        }
    }
}
