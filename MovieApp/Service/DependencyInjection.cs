using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Service.Storage;
using MovieApp.Service.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ITableStorage, TableStorage>();

            return services;
        }
    }
}
