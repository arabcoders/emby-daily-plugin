using System;
using DailyExtender.Helpers;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Serialization;

namespace DailyExtender
{
    public class Plugin : BasePlugin
    {
        public override string Name => Constants.PLUGIN_NAME;
        private readonly ILogger _logger;
        public override Guid Id => Guid.Parse(Constants.PLUGIN_GUID);
        public Plugin(ILogManager logManager)
        {
            _logger = logManager.GetLogger(Name);
            _logger.Info("Loaded Daily Extender Plugin.");
        }
        public override string Description => "Extends daily episode date format.";
    }
}
