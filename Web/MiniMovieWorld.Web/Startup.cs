namespace MiniMovieWorld.Web
{
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MiniMovieWorld.Data;
    using MiniMovieWorld.Data.Common;
    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Data.Repositories;
    using MiniMovieWorld.Data.Seeding;
    using MiniMovieWorld.Services.Data;
    using MiniMovieWorld.Services.Data.Admin.ActorsService;
    using MiniMovieWorld.Services.Data.Admin.CategoriesService;
    using MiniMovieWorld.Services.Data.Admin.DirectorsService;
    using MiniMovieWorld.Services.Data.Admin.MoviesService;
    using MiniMovieWorld.Services.Data.Admin.WritersService;
    using MiniMovieWorld.Services.Data.User;
    using MiniMovieWorld.Services.Data.User.ActorsService;
    using MiniMovieWorld.Services.Data.User.DirectorsService;
    using MiniMovieWorld.Services.Data.User.ProducersService;
    using MiniMovieWorld.Services.Data.User.RatesService;
    using MiniMovieWorld.Services.Data.User.UsersService;
    using MiniMovieWorld.Services.Data.ViewComponents;
    using MiniMovieWorld.Services.Mapping;
    using MiniMovieWorld.Services.Messaging;
    using MiniMovieWorld.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IActorsService, ActorsService>();
            services.AddTransient<IProducersService, ProducersService>();
            services.AddTransient<IDirectorsService, DirectorsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IMoviesService, MoviesService>();
            services.AddTransient<IUserMoviesService, UserMoviesService>();
            services.AddTransient<IUserActorsService, UserActorsService>();
            services.AddTransient<IUserProducersService, UserProducersService>();
            services.AddTransient<IUserDirectorsService, UserDirectorsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRatesService, RatesService>();
            services.AddTransient<IViewComponentsService, ViewComponentsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder(userManager, roleManager).SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            roleManager.CreateAsync(new ApplicationRole("User"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
