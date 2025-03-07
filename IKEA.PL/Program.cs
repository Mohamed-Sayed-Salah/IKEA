using IKEA.BLL.Services;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
            
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>((optionsBuilder => 
            {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }));
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); 
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
