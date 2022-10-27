using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScooterRentalAPI.Core.Models;
using ScooterRentalAPI.Core.Services;
using ScooterRentalAPI.Core.Validations;
using ScooterRentalAPI.Data;
using ScooterRentalAPI.Services;

namespace ScooterRentalAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScooterRentalAPI", Version = "v1" });
            });

            services.AddDbContext<ScooterRentalDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("scooter-rental"));
            });

            services.AddScoped<IScooterRentalDbContext, ScooterRentalDbContext>();

            services.AddScoped<Calculator>();
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IEntityService<Scooter>, EntityService<Scooter>>();
            services.AddScoped<IEntityService<RentedScooter>, EntityService<RentedScooter>>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IScooterService, ScooterService>();

            services.AddScoped<IScooterValidator, ScooterValidator>();
            services.AddScoped<IScooterValidator, ScooterNameValidator>();
            services.AddScoped<IScooterValidator, ScooterPriceValidator>();

            //// Default Policy
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder =>
            //        {
            //            builder.WithOrigins("https://localhost:7070", "http://localhost:4200")
            //                .AllowAnyHeader()
            //                .AllowAnyMethod();
            //        });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScooterRentalAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
