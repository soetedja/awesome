using Awesome.Common;
using Awesome.Repository;
using Microsoft.EntityFrameworkCore;

namespace Awesome.Api
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
            services.AddSwaggerGen();

            DependencyRegister.RegisterInternalServiceDependencies(services);
            DependencyRegister.RegisterExternalServiceDependencies(services);

            if (!UnitTestDetector.IsRunningFromXUnit())
            {
                services.AddDbContext<DataContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

                services.AddScoped<IDataContext>(s => s.GetService<DataContext>()!);
            }

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dbContext)
        {

            if (!UnitTestDetector.IsRunningFromXUnit())
            {
                dbContext.Database.EnsureCreated();
            }

            bool dataInitialized = dbContext.AppSettings.Any(s => s.Name == "DataInitialized" && s.Value == "true");
            if (!dataInitialized)
            {
                // Initialize data
                dbContext.InitializeData();

                // Update flag to indicate that data has been initialized
                var flag = dbContext.AppSettings.FirstOrDefault(s => s.Name == "DataInitialized");
                if (flag != null)
                {
                    flag.Value = "true";
                    dbContext.SaveChanges();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
