using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonRegistration.Application;
using PersonRegistration.Domain.Core;
using PersonRegistration.Domain.Persons.Repository;
using PersonRegistration.Domain.Persons.Services;
using PersonRegistration.Infrastructure.Data;

namespace PersonRegistration.Infrastructure.CrossCutting
{
    public static class BootStrapper
    {
        public static void RegistreServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, PersonDbContext>();
            services.AddDbContext<PersonDbContext>((options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<NotificationContainer>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonAppService, PersonAppService>();
        }
    }
}
