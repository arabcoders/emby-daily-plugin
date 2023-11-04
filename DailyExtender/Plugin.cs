using System;
using DailyExtender.Configuration;
using DailyExtender.Helpers;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Serialization;

namespace DailyExtender
{
    public class Plugin : BasePlugin<PluginConfiguration>
    {
        public override string Name => Constants.PLUGIN_NAME;
        public static Plugin Instance { get; private set; }
        public override Guid Id => Guid.Parse(Constants.PLUGIN_GUID);
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }
    }
}
