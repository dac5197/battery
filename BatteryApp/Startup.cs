using BatteryApp.Areas.Identity;
using BatteryApp.Data;
using BatteryApp.Internals;
using BatteryApp.Internals.Validators;
using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.NoteModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using BatteryApp.Models.TagModel;
using BatteryApp.Models.UserProfileModel;
using BatteryApp.Views.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContextFactory<AppDbContextFactory>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages(options => options.RootDirectory = "/Views/Pages");
            services.AddServerSideBlazor();
            //services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSingleton<WeatherForecastService>();

            // Add SignalR Service if in production environment
            if (_env.IsProduction())
            {
                services.AddSignalR().AddAzureSignalR(Configuration["Azure:SignalR:ConnectionString"]);
            }

            // Add Email Sender and get vaules from appsettings.json and secrets
            services.AddTransient<IEmailSender, EmailSender>();

            // Add Project Models CRUD Services
            services.AddTransient<IBatteryService, BatteryService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IChargeService, ChargeService>();
            services.AddTransient<IChargeCountService, ChargeCountService>();
            services.AddTransient<IChargeTagRelationService, ChargeTagRelationService>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<INoteTypeService, NoteTypeService>();
            services.AddTransient<IPriorityService, PriorityService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IUserProfileService, UserProfileService>();

            // Add Project Internals
            services.AddTransient<IBatteryAdvancedOptionsSaveService, BatteryAdvancedOptionsSaveService>();
            services.AddTransient<IChargeChildController, ChargeChildController>();
            services.AddTransient<IChargeLifecycle, ChargeLifecycle>();
            services.AddTransient<IDeleteChargeController, DeleteChargeController>();
            services.AddTransient<IInitializeChargeChildToParent, InitializeChargeChildToParent>();
            services.AddTransient<ITagController, TagController>();

            // Add Project View Utils
            services.AddTransient<ICalculateTextareaRows, CalculateTextareaRows>();
            services.AddTransient<ITableSort_Charges, TableSort_Charges>();
            services.AddScoped<IBreadcrumbManager, BreadcrumbManager>();
            services.AddScoped<IChargeOpenChildrenModalHelper, ChargeOpenChildrenModalHelper>();
            services.AddScoped<ISearchUtil, SearchUtil>();

            // Add Project Validators
            services.AddTransient<IBatteryAdvancedOptionsValidator, BatteryAdvancedOptionsValidator>();
            services.AddTransient<ICategoryValidator, CategoryValidator>();
            services.AddTransient<IIdentityRoleValidator, IdentityRoleValidator>();
            services.AddTransient<IIdentityUserValidator, IdentityUserValidator>();
            services.AddTransient<IPriorityValidator, PriorityValidator>();
            services.AddTransient<IStatusValidator, StatusValidator>();
            services.AddTransient<ITagValidator, TagValidator>();

            // Radzen Services
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
