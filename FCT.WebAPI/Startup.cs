using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCT.WebAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace FCT.WebAPI {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<DataContext> (x => x.UseInMemoryDatabase (databaseName: "Item"));
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1).AddXmlDataContractSerializerFormatters ();
            services.AddCors ();

            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info { Title = "Test - Formulatrix API", Version = "v1" });
            });
            services.AddScoped<IItemRepo, ItemRepo> ();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            app.UseCors (x => x.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ());

            app.UseSwagger ();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Test - Formulatrix API V1");
            });

            app.UseHttpsRedirection ();
            app.UseMvc ();

        }
    }
}