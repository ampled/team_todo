using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using TeamTodo.Models;
using AutoMapper;
using TeamTodo.ViewModels;

namespace TeamTodo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<TodoContext>(options => 
                options.UseSqlite("Filename=./testdb_001.db"));
                
            services.AddTransient<TodoSeedData>();
            
            services.AddMvc()
                .AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
                //options.SerializerSettings.PreserveReferencesHandling = 
                //    Newtonsoft.Json.PreserveReferencesHandling.Objects;
            });
            
            services.AddScoped<ITodoUserRepository, TodoUserRepository>();
            services.AddScoped<ITodoTypeRepository, TodoTypeRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, TodoSeedData seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
            Mapper.Initialize(config => {
                config.CreateMap<TodoUser, TodoUserViewModel>().ReverseMap();
                config.CreateMap<TodoType, TodoTypeViewModel>().ReverseMap();
                config.CreateMap<Todo, TodoViewModel>().ReverseMap();
                config.CreateMap<Todo, NestedTodoViewModel>().ReverseMap();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Todos}/{id?}");
            });
            
            seeder.EnsureSeedData();
        }
    }
}
