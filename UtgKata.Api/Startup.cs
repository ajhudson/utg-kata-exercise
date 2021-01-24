// <copyright file="Startup.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using UtgKata.Api.Filters;
    using UtgKata.Api.Services;
    using UtgKata.Data;
    using UtgKata.Data.Models;
    using UtgKata.Data.Repositories;

    /// <summary>
    ///   The startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>Initializes a new instance of the <see cref="Startup" /> class.</summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>Gets the configuration.</summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>Configures the services.</summary>
        /// <param name="services">The services.</param>
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

        /// <summary>Configures the specified application.</summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
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
