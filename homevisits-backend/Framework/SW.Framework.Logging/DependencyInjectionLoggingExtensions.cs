#define DEBUG
using Common.Logging;
using Common.Logging.Serilog;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace SW.Framework.LoggingCore
{
    /// <summary>
    ///     Class DependencyInjectionLoggingExtensions.
    /// </summary>
    public static class DependencyInjectionLoggingExtensions
    {
        public static void RegisterDefaultLogger(this IServiceCollection services, string path = null)
        {


#if DEBUG
            if (path == null)
                Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
            else
                Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.RollingFile(path).CreateLogger();
#endif
          
            services.AddSingleton<ILogger>(Log.Logger);
            services.AddTransient<ILog, SerilogCommonLogger>();
        }
    }
}