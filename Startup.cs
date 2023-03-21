using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SendEmailFunction.Configuration;
using SendEmailFunction.Services;

[assembly: FunctionsStartup(typeof(SendEmailFunction.Startup))]
namespace SendEmailFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<SendEmailService>();
            builder.Services.AddSingleton<MailSettings>();
        }
    }
}
