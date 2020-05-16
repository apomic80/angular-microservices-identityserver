// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Linq;
using identityserver.Services;
using identityserver.Storage;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Quickstart.UI;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.Conventions;

namespace identityserver
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer(options =>
            {
                options.IssuerUri = Configuration.GetValue<string>("ISSUER_URI");
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddMongoRepository(
                Configuration.GetValue<string>("MONGO_CONNECTION"),
                Configuration.GetValue<string>("MONGO_DATABASE_NAME"))
            .AddClients()
            .AddIdentityApiResources()
            .AddPersistedGrants();
            
            seedDatabase(services);
            
            builder.AddProfileService<ProfileService>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddSingleton<ICorsPolicyService, RepositoryCorsPolicyService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void seedDatabase(IServiceCollection services)
        {
            configureMongoDriverIgnoreExtraElements();

            var sp = services.BuildServiceProvider();
            var repository = sp.GetService<IRepository>();

            if (repository.All<Client>().Count() == 0)
            {
                foreach (var client in Config.Clients)
                {
                    repository.Add<Client>(client);
                }
            }

            if (repository.All<IdentityResource>().Count() == 0)
            {
                foreach (var res in Config.Ids)
                {
                    repository.Add<IdentityResource>(res);
                }
            }

            if (repository.All<ApiResource>().Count() == 0)
            {
                foreach (var api in Config.Apis)
                {
                    repository.Add<ApiResource>(api);
                }
            }

            if (repository.All<TestUser>().Count() == 0)
            {
                foreach (var user in TestUsers.Users)
                {
                    repository.Add<TestUser>(user);
                }
            }
        }

        private void configureMongoDriverIgnoreExtraElements()
        {
            var pack = new ConventionPack();
            pack.Add(new IgnoreExtraElementsConvention(true));
            ConventionRegistry.Register("IdentityServer Mongo Conventions", pack, t => true);
        }
    }
}