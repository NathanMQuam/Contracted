using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Repositories;
using Services;

namespace Contracted
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
         //NOTE this is required for using auth
         services.AddAuthentication(options =>
                {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                   options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                   options.Audience = Configuration["Auth0:Audience"];
                });

         //NOTE this is for communicating with client
         services.AddCors(options =>
           {
              options.AddPolicy("CorsDevPolicy", builder =>
               {
                  builder
                         .WithOrigins(new string[]{
                            "http://localhost:8080",
                            "http://localhost:8081"
                           })
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials();
               });
           });

         services.AddScoped<IDbConnection>(x => CreateDbConnection());

         services.AddTransient<JobsService>();
         services.AddTransient<JobsRepository>();
         services.AddTransient<ContractorsService>();
         services.AddTransient<ContractorsRepository>();
         services.AddTransient<AssignmentsService>();
         services.AddTransient<AssignmentsRepository>();

         services.AddControllers();
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contracted", Version = "v1" });
         });
      }

      private IDbConnection CreateDbConnection()
      {
         var connectionString = Configuration["db:gearhost"];
         return new MySqlConnection(connectionString);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contracted v1"));
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
