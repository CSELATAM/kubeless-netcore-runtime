﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Kubeless.Core.Interfaces;
using Kubeless.WebAPI.Utils;

namespace kubeless_netcore_runtime
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
            services.AddMvc();

            services.AddSingleton<IFunctionSettings>(FunctionFactory.BuildFunctionSettings(Configuration));
            services.AddSingleton<IFunction>(FunctionFactory.BuildFunction(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Set fixed enviroment variables for example function:
                Environment.SetEnvironmentVariable("MOD_NAME", "mycode");
                Environment.SetEnvironmentVariable("FUNC_HANDLER", "execute");
            }

            app.UseMvc();
        }
    }
}
