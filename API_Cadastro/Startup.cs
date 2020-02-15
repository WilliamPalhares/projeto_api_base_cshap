using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API_Cadastro.Interfaces;
using API_Cadastro.Models;
using API_Cadastro.Repositories;
using API_Cadastro.Service;
using API_Cadastro.Singletons;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace API_Cadastro
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        public ILogger Logger { get; }
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "AllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationApplication application = Configuration.GetSection("ConfigurationApplication").Get<ConfigurationApplication>();
            services.AddSingleton(application);

            services.AddDistributedMemoryCache();

            services.AddMvc()
                    .AddJsonOptions(op =>
                    {
                        op.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                        op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<GzipCompressionProviderOptions>(op => op.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(op =>
            {
                op.Providers.Add<GzipCompressionProvider>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info { Title = "API Base", Version = "v1" });
            });

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(application.ConnectionString));
            services.AddScoped<IPaisSingleton, PaisSingleton>();
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "*", "*")
                           .WithHeaders()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider ser)
        {
            app.UseSwagger();

            app.UseCors(MyAllowSpecificOrigins);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Base V1");
            });

            app.UseResponseCompression();

            app.UseHttpsRedirection();
            app.UseMvc();

            

            ser.GetService<IDataService>().InicializaDB();
        }
    }
}
