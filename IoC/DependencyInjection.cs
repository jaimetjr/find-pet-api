using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Security;
using Application.Services;
using Azure.Storage.Blobs;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAllDependencies(this IServiceCollection services, IConfigurationManager configuration)
        {
            //services.AddInfrastructureServices(configuration);
            //services.AddApplicationServices(configuration);

            services.AddDbContext<AppDataContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationHandler, SelfOrElevatedHandler>();
            services.AddScoped<IFileStorageService, AzureBlobStorageService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPetRepository, PetRepository>();

            services.AddSingleton(serviceProvider =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = config["AzureBlobStorage:ConnectionString"];
                var containerName = config["AzureBlobStorage:ContainerName"];

                var serviceClient = new BlobServiceClient(connectionString);
                return serviceClient.GetBlobContainerClient(containerName);
            });

            return services;
        }

    }
}
