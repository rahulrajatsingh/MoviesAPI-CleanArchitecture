using Movies.Core.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Logging;

public class Logger : Movies.Core.Logging.ILogger
{
    private readonly Serilog.ILogger _logger;

    public Logger()
    {
        _logger = Log.Logger;
    }

    public void LogInfo(string message) => _logger.Information(message);
    public void LogWarn(string message) => _logger.Warning(message);
    public void LogDebug(string message) => _logger.Debug(message);
    public void LogError(string message) => _logger.Error(message);
}
