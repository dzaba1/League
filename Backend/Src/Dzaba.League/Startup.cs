using Dzaba.League.DataAccess.EntityFramework;
using Dzaba.League.DataAccess.EntityFramework.Sqlite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Dzaba.AspNet.Auth;
using Dzaba.AspNet.Jwt;
using Dzaba.League.DataAccess.Contracts.Model;
using Dzaba.Utils;

namespace Dzaba.League
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.RegisterUtils();
            services.RegisterEntityFrameworkDataAccess();
            services.RegisterSqlite();
            services.RegisterIdentityAuth<User, long>();
            services.RegisterJwtAuth();
            services.RegisterWebApi();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
