using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Persistence.Contracts;
using HRLeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LeaveManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("LeaveManagementConnectionString")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            return services;

        }
    }
}
