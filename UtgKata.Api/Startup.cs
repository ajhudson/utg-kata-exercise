using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UtgKata.Data;
using UtgKata.Data.Models;
using UtgKata.Data.Repositories;
using AutoMapper;
using UtgKata.Api.Filters;
using UtgKata.Api.Services;

namespace UtgKata.Api
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
            services.AddControllers(opts => opts.Filters.Add<GeneralResponseViewResultFilterAttribute>());
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<UtgKataDbContext>();
                optionsBuilder.UseInMemoryDatabase(DbContextSettings.DatabaseName);

                return new UtgKataDbContext(optionsBuilder.Options);
            });

            services.AddTransient<IRepository<Customer>, GeneralRepository<Customer>>();
            services.AddTransient<ICustomerService, CustomerService>();
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
