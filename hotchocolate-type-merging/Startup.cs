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

            /*
            Setup local schema. The local schema provides some root
            queries of its own and also extends a remove type (Ablum).
            */
            services
            .AddGraphQLServer("myschema")
            .AddType<Query>();

            /*
            Add local and remote (stitch) schema.
            The extension file will provide information about the
            delegation of the extended type (custom field).
            */
            services
            .AddGraphQLServer()
            .AddRemoteSchema("almansi")
            .AddLocalSchema("myschema")
            .AddTypeExtensionsFromFile("./Extensions.graphql");
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
