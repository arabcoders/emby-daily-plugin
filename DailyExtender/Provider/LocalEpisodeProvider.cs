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
            var result = new MetadataResult<Episode>();

            // ignore youtube content due to it's overriding yt-info reader provider.
            if (Utils.IsYouTubeContent(info.Path))
            {
                _logger.Debug($"LEP GetMetadata: Ignoring Path {info.Path}");
                return Task.FromResult(result);
            }
            // ignore non daily content.
            if (!Utils.IsDailyContent(info.Path))
            {
                _logger.Debug($"LEP GetMetadata: Ignoring Non daily content {info.Path}");
                return Task.FromResult(result);
            }

            var dto = Utils.Parse(info.Path);

            if (dto == null || dto.Year == null)
            {
                _logger.Debug($"LEP GetMetadata: No data was found {dto.Year}");
                return Task.FromResult(result);
            }

            result = Utils.DTOToEpisode(dto);

            _logger.Debug($"LEP GetMetadata: Parsed data {dto}");

            return Task.FromResult(result);
        }
    }
}
