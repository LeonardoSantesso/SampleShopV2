using System;
using Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

[assembly: FunctionsStartup(typeof(SampleShopV2.Startup))]

namespace SampleShopV2
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;

            services.AddDbContext<SampleShopV2Context>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString")));

            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddSingleton<IAuditService, AuditService>();
            services.AddSingleton<ILogger, Logger>();

            builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly);
        }
    }
}