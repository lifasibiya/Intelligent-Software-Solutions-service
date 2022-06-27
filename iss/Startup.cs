using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using iss.Data;
using Microsoft.Extensions.DependencyInjection;
using iss.Models;

namespace iss
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigueServices(IServiceCollection services)
        {
            services.AddMvc();
            var conn = Configuration["ConnectionStrings:iss"];
            services.AddDbContextPool<ISSDBContext>(option => option.UseSqlServer(conn));
            services.AddTransient<IDocumentService, DocumentManagement>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
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
