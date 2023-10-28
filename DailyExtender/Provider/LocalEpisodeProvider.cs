using System.Threading;
using System.Threading.Tasks;
using DailyExtender.Helpers;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Configuration;
using MediaBrowser.Model.Logging;

namespace DailyExtender.Provider
{
    public class LocalEpisodeProvider : ILocalMetadataProvider<Episode>
    {
        protected readonly ILogger _logger;

        public string Name => Constants.PLUGIN_NAME;

        public LocalEpisodeProvider(ILogManager logManager)
        {
            _logger = logManager.GetLogger(GetType().Name);
        }

        public Task<MetadataResult<Episode>> GetMetadata(ItemInfo info, LibraryOptions libraryOptions, IDirectoryService directoryService, CancellationToken cancellationToken)
        {
            var dto = Utils.Parse(info.Path);

            if (dto == null || dto.Year == null)
            {
                return Task.FromResult(new MetadataResult<Episode>());
            }

            return Task.FromResult(Utils.DTOToEpisode(dto));
        }
    }
}
