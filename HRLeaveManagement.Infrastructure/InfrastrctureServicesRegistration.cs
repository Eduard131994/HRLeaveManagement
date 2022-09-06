using HRLeaveManagement.Application.Contracts.Infrastructure;
using HRLeaveManagement.Application.Models;
using HRLeaveManagement.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Infrastructure
{
    public static class InfrastrctureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }
    }
}
