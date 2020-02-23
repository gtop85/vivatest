using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using vivatest.controllers;
using vivatest.DAL;
using vivatest.fixer;
using vivatest.services;

namespace vivatest.api
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
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHttpClient();
            services.AddSingleton<IFixerService, FixerService>();
            services.AddSingleton<IFinancialRecordRepository, FinancialRecordRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IFinancialRecordsService, FinancialRecordsService>();

            RegisterSwagger(services);
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "viva test API",
                    Version = "v1",
                    Description = "This is an API created for a case study provided by viva wallet. The project can be found [here](https://github.com/gtop85/vivatest)" +
                    "\n\nWith this API you can: \n1.\tCreate new records\n2.\tSearch for records\n3.\tUpdate a record\n4.\tDelete a record\n5.\tGet a report for records" +
                    "\n\nAcceptable types for each category are:" +
                    "\n\n**Product**\n1.\tAmarilla\n2.\tCarretera\n3.\tMontana\n4.\tPaseo\n5.\tVelo\n6.\tVTT" +
                    "\n\n**Segment**\n1.\tEnterprise\n2.\tChannel Partners\n3.\tGovernment\n4.\tMidmarket\n5.\tSmall Business" +
                    "\n\n**Country**\n1.\tCanada\n2.\tFrance\n3.\tGermany\n4.\tMexico\n5.\tUnited States of America" +
                    "\n\n**DiscountBand**\n1.\tHigh\n2.\tMedium\n3.\tLow\n4.\tNone" +
                    ""
                    ,
                    Contact =  new OpenApiContact
                    {
                        Name = "George Topcharas",
                        Email = "george.topcharas@gmail.com"
                    }
    
                });

                // Set the comments path for the Swagger JSON and UI.
                FinancialRecordsController dummyInstance;
                var currentAssembly = Assembly.GetExecutingAssembly();

                var ReferencedAssemblies = currentAssembly.GetReferencedAssemblies();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();

                Array.ForEach(xmlDocs, (d) =>
                {
                    c.IncludeXmlComments(d);
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "API V1");
            });

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
    }
}
