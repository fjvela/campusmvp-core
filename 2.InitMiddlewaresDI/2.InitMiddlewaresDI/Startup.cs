using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InitMiddlewaresDI.Code;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _2.InitMiddlewaresDI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            SetupDI(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAdder addImpl)
        {
            Envioronments(app, env);
            //middelwares
            Middelwares(app, addImpl);



        }

        private void SetupDI(IServiceCollection services)
        {
            services.AddTransient<IAdder, BasicCalculator>();
        }

        private static void Envioronments(IApplicationBuilder app, IHostingEnvironment env)
        {
            //by default 3 environment, you can create more env.IsEnvironment∫
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* app.Run(async (context) =>
             {
                 var msgEnv = $"Hello from enviroent {env.EnvironmentName}";
                 await context.Response.WriteAsync(msgEnv);
             });
 */
        }

        private static void Middelwares(IApplicationBuilder app, IAdder addImpl)
        {

            app.Use(async (ctx, nex) =>
            {
                string result;
                if (ctx.Request.Path == "/add") {

                    if (int.TryParse(ctx.Request.Query["a"], out int a) &&
                        int.TryParse(ctx.Request.Query["b"], out int b))
                    {

                        //var addImpl = app.ApplicationServices.GetService<IAdder>(); // => noo you must give it on ctr
                        result = addImpl.Add(a, b);
                    }
                    else result = $"Values aren't valid. Please check it a:{ctx.Request.Query["a"]} b:{ctx.Request.Query["b"]}";

                }else {
                    result = $"Operation {ctx.Request.Path} can't be executed";
                }
                await ctx.Response.WriteAsync(result);
            });

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Path.ToString().ToLower().StartsWith("/hello"))
                {
                    await ctx.Response.WriteAsync("Hello, I'm the first middelware the others will not executed. Take care in setup!");
                }
                else
                {
                    await next();
                }
            });


            // Request Info middleware
            app.Run(async ctx =>
            {
                await ctx.Response.WriteAsync($"Path requested: {ctx.Request.Path}");
            });

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Path == "/hello-world")
                {
                    // process the request and stop the next middlewares 
                    await ctx.Response.WriteAsync("Hello, world!");
                }
                else
                {
                    //we skip the request and the next middleware proccess it
                    await next();
                }
            });

            // Request Info middleware
            app.Run(async ctx =>
            {
                await ctx.Response.WriteAsync($"Path requested: {ctx.Request.Path}");
            });
        }
    }
}
