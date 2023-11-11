using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;

namespace Api.Configurations;

public static class LogConfig
{
    public static IServiceCollection AddLogConfiguration(this IServiceCollection services)
    {
        services.AddLogging(provider =>
        {
            provider.AddKissLog(options =>
            {
                options.Formatter = (FormatterArgs args) =>
                {
                    if (args.Exception == null)
                        return args.DefaultValue;

                    string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);
                    return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
                };
            });
        });

        services.AddHttpContextAccessor();

        return services;
    }

    public static IApplicationBuilder UseLogConfig(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseKissLogMiddleware(options =>
        {
            options.Listeners.Add(new RequestLogsApiListener(new Application(
                configuration["KissLog.OrganizationId"],
                configuration["KissLog.ApplicationId"])
            )
            {
                ApiUrl = configuration["KissLog.ApiUrl"]
            });
        });

        return app;
    }
}
