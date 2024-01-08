using Funq;
using ServiceStack;
using PaySocialCredit.ServiceInterface;
using PaySocialCredit.ServiceModel.CreateUserReferenceModels;
using PaySocialCredit.ServiceModel.CreateUserReferenceService;
using Serilog;
using Serilog.Core;
using ServiceStack.Validation;

[assembly: HostingStartup(typeof(PaySocialCredit.AppHost))]

namespace PaySocialCredit;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services =>
        {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("PaySocialCredit", typeof(MyServices).Assembly)
    {
    }

    public override void Configure(Container container)
    {
        // enable server-side rendering, see: https://sharpscript.net/docs/sharp-pages
        Plugins.Add(new SharpPagesFeature
        {
            EnableSpaFallback = true
        });
        Plugins.Add(new ValidationFeature() { ScanAppHostAssemblies = false });
        SetConfig(new HostConfig
        {
            AddRedirectParamsToQueryString = true,
        });
        container.AddSingleton<ResponseProxy>();
        addEnabledServices(container);
        addValidators(container);
        addLogger(container);
        Plugins.Add(new CorsFeature(allowOriginWhitelist: new[]
        {
            "http://localhost:5000",
            "http://localhost:3000",
            "http://localhost:8080",
            "https://localhost:5001",
        }, allowCredentials: true));
    }

    private static void addValidators(Container container)
    {
    
        container.RegisterValidators(typeof(UserReferenceValidator).Assembly);
    }

    private static void addLogger(Container container)
    {
        var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.File("logs/log.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        container.AddSingleton<Logger>(a => logger);
    }

    private static void addEnabledServices(Container container)
    {
        var services = new EnabledServices();
        services.Add(typeof(CreateUserReferenceRequest));
        container.AddSingleton(c => services);
    }
}