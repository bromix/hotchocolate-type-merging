using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// query{album(id: "1"){title}}

namespace hotchocolate_type_merging
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient("almansi", c =>
            {
                c.BaseAddress = new Uri("https://graphqlzero.almansi.me/api");
            });

            services
            .AddGraphQLServer("myschema")
            .AddType<Query>();


            services
            .AddGraphQLServer()
            .AddRemoteSchema("almansi")
            .AddTypeExtensionsFromFile("./Extensions.graphql")
            .AddLocalSchema("myschema");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
