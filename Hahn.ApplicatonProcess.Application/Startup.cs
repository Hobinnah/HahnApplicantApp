using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.May2020.Data;
using Hahn.ApplicatonProcess.May2020.Domain;
using Hahn.ApplicatonProcess.May2020.Domain.BusinessLogic;
using Hahn.ApplicatonProcess.May2020.Domain.BusinessLogic.Implementation;
using Hahn.ApplicatonProcess.May2020.Domain.BusinessLogic.Interfaces;
using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Hahn.ApplicatonProcess.May2020.Domain.ModelValidators;
using Hahn.ApplicatonProcess.May2020.Domain.Repositories.Implementations;
using Hahn.ApplicatonProcess.May2020.Domain.Repositories.Interfaces;
using Hahn.ApplicatonProcess.May2020.Domain.Responses;
using Hahn.ApplicatonProcess.May2020.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Hahn.ApplicatonProcess.Application
{

    // Microsoft.EntityFrameworkCore.InMemory
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
            services.AddMvc(setup => {  }).AddFluentValidation();
            services.AddDbContext<AppDBContext>(options => options.UseInMemoryDatabase(databaseName: "ApplicantDB"));

            #region ======================== ID =============================
            services.AddTransient<IValidator<Applicant>, ApplicantValidator>();
            services.AddSingleton<IConfigurationFile, ConfigurationFile>();
            services.AddTransient<IApplicantRepository, ApplicantRepository>();
            services.AddTransient<IApplicantService, ApplicantService>();
            services.AddMvc(opt =>{ opt.Filters.Add(typeof(ValidatorActionFilter)); });

           
            #endregion

            #region ===== Swagger generator =====
            //Register the Swagger generator, defining 1 or more Swagger documents
            // Swashbuckle.AspNetCore
            object p = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Hahn Applicants Web API",
                    Description = "Hahn Applicants Web API Suits, an ASP.NET Core Web API",
                    TermsOfService = new Uri("http://Hahn.com/applicants/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Eze Obinna J",
                        Email = string.Empty,
                        Url = new Uri("https://Hahn.com/applicants/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://Hahn.com/applicants/license"),
                    }
                });
            });

            #endregion

            #region ===== CORS configuration =====
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.SetIsOriginAllowed((host) => true).AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:8080", "").AllowCredentials().Build();
                });

            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            // http://localhost:56404/swagger/index.html

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Applicants Core WebAPI");
            });

            //.AddFilter("Microsoft", LogLevel.Information)
            //.AddFilter("System", LogLevel.Error)
            app.UseCors("EnableCORS");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
