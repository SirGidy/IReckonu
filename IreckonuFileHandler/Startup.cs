using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IreckonuFileHandler.Core;
using IreckonuFileHandler.Core.Repositories;
using IreckonuFileHandler.Core.Services;
using IreckonuFileHandler.Services.Persistence.Contexts;
using IreckonuFileHandler.Services.Repositories;
using IreckonuFileHandler.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace IreckonuFileHandler
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


          
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductPathRepository, ProductPathRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IDiskService, DiskService>();

            services.AddScoped<ILogServices, LogServices>();


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });


            // add content negotiation
            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;


            }).AddXmlSerializerFormatters().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);



            ConfigureDatabase(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "IRECKONU File Upload API Documentation", Description = "", TermsOfService = "None", Contact = new Contact() { Name = "IRECKONU", Email = "gojemakinde@gmail.com" } });

                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"IreckonuFileHandler.xml";
                c.IncludeXmlComments(xmlPath);

            });


            
        }



        public virtual void ConfigureDatabase(IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("recipe-test-in-memorymain"));


         
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "IRECKONU File Upload API Documentation");
            }

            );

            app.UseMvc();
        }
    }
}
