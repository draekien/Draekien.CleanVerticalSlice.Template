using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Common.Api.Configuration;
using Common.Api.Filters;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Converters;

using Serilog;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Common.Api
{
    public static class DependencyInjection
    {
        public static void AddCommonApi(this IServiceCollection services, IHostEnvironment env, string apiTitle, IEnumerable<Assembly> assemblies)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.UseCamelCasing(true);
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddHealthChecks();

            services.AddProblemDetailMaps(env);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = apiTitle, Version = "v1" });

                foreach (var assembly in assemblies)
                {
                    var xmlFile = $"{assembly.GetName().Name}.xml";
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    c.IncludeXmlComments(xmlPath, true);
                }

                c.EnableAnnotations();

                c.DocInclusionPredicate((_,_) => true);
                c.AddFluentValidationRules();

                c.ExampleFilters();
                c.OperationFilter<AddCorrelationIdHeaderParameter>();
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void UseCommonApi(this IApplicationBuilder app, IWebHostEnvironment env, string apiName)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", apiName));
                app.UseReDoc(c =>
                {
                    c.RoutePrefix = "docs";
                    c.DocumentTitle = apiName;
                    c.SpecUrl = "/swagger/v1/swagger/json";
                    c.ExpandResponses("200,201");
                    c.RequiredPropsFirst();
                    c.SortPropsAlphabetically();
                });
            }

            app.UseHealthChecks("/health");
            app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = (context, httpContext) =>
                {
                    context.Set("RequestHost", httpContext.Request.Host.Value);
                    context.Set("RequestScheme", httpContext.Request.Scheme);
                };
            });
            app.UseProblemDetails();
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
