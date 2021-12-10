using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Web.API.Helper;
using Web.Data;
using Web.Data.Concrete;
using Web.Data.Db_Context;
using Web.Data.Generic_Repository;
using Web.Data.Interfaces;
using Web.Services.Concrete;
using Web.Services.Interfaces;

namespace Web.API
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

            services.AddDbContext<DbHRMSContext>(options =>
              options.UseSqlServer(
                 Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
         
            /*services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });*/

            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "HRMS API",
                    // Description = "API with ASP.NET Core",
                    //Contact = new OpenApiContact()
                    //{
                    //    Name = "HRMS",
                    //    Url = new Uri("https://localhost:44390/")
                    //}
                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                c.AddSecurityRequirement(securityRequirement);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = Configuration["Jwt:ValidAudience"],
                     ValidAudience = Configuration["Jwt:ValidAudience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]))
                 };
             });

            //Register Services
            services.AddDbContext<DbHRMSContext>();
            services.AddScoped<DbContext>(sp => sp.GetService<DbHRMSContext>());

            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddHttpContextAccessor();
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddTransient(typeof(IJwtAuthService), typeof(AuthService));        
            services.AddTransient(typeof(IEmployeeService), typeof(EmployeeService));
            services.AddTransient(typeof(IAssetLaptopService), typeof(AssetLaptopService));

            //Register Services Repositories
            services.AddTransient(typeof(IHRMSUserAuthRepository), typeof(HRMSUserAuthRepository));
            services.AddTransient(typeof(IHRMSEmployeeRepository), typeof(HRMSEmployeeRepository));
            //Register Assest Service 
            services.AddTransient(typeof(IHRMSIMSAssetRepository), typeof(HRMSAssetRepository));
            services.AddTransient(typeof(IHRMSIMSAssetLaptopRepository), typeof(HRMSAssetLaptopRepository));

            services.AddTransient(typeof(IHRMSIMSAssetACRepository), typeof(HRMSAssetACRepository));

            services.AddTransient(typeof(IHRMSIMSAssetFurnitureRepository), typeof(HRMSAssetFurnitureRepository));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            /*if (env.IsDevelopment())
            {*/
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestingAPIForHawklogix v1"));
            /*}*/

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseCors(x => x
              .SetIsOriginAllowed(origin => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
