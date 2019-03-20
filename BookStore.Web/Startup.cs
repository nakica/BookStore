namespace BookStore.Web
{
    using BookStore.Repository;
    using BookStore.Repository.Contracts;
    using BookStore.Services;
    using BookStore.Services.Contracts;
    using IdentityCore.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddIdentity<IdentityUser, IdentityRole>(
                options => options.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<BookStoreContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddTransient<DbContext, BookStoreContext>();

            services.AddTransient<IBookRepository, BookRepository>();

            services.AddTransient<IBookService, BookService>();


            services.AddDbContext<BookStoreContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BookStoreContext context,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("BookStore/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=BookStore}/{action=Index}");
            }
            );
            InitializeRoles.Initialize(context, userManager, roleManager).Wait();
        }
    }
}
