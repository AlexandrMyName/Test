using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TadWhat.Encoding;
using TadWhat.Repository;
using TadWhat.Services;

namespace TadWhat
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfigurationRoot conectionString_Database;
        public IConfigurationRoot api_dadata;

        public Startup(IConfiguration configuration, IHostEnvironment env){

            Configuration = configuration;
            conectionString_Database = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("database.json").Build();
            api_dadata = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("dataToken.json").Build();
        }
        public void ConfigureServices(IServiceCollection services){

            services.AddTransient<IAddress, AddreseRussian>();
            services.AddTransient<ICrypto, CryptoDadata>();
            services.AddTransient<AddreseRussian>();
            services.AddMvc();
            services.AddTransient<UserRepo>();
            services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(conectionString_Database.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICrypto crypto){

            crypto.Secret = api_dadata.GetConnectionString("secret");
            crypto.Token = api_dadata.GetConnectionString("token");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else{
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
