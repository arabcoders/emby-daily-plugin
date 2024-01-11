using System;
using System.Threading;
using System.Threading.Tasks;
using DailyExtender.Helpers;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Configuration;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.IO;

namespace DailyExtender.Provider
{
    public class DailyExtenderProvider : ILocalMetadataProvider<Episode>, IHasItemChangeMonitor
    {
        protected readonly ILogger _logger;

        public string Name => Constants.PLUGIN_NAME;

        public DailyExtenderProvider(ILogManager logManager)
        {
            _logger = logManager.GetLogger(GetType().Name);
        }

        public Task<MetadataResult<Episode>> GetMetadata(ItemInfo info, LibraryOptions libraryOptions, IDirectoryService directoryService, CancellationToken cancellationToken)
        {
            var result = new MetadataResult<Episode>();

            // ignore non daily content.
            if (!Utils.IsDailyContent(info.Path))
            {
                _logger.Debug($"DEP GetMetadata: Ignoring Non-daily content. {info.Path}");
                return Task.FromResult(result);
            }

            var dto = Utils.Parse(info.Path);

            if (dto == null || dto.Year == null)
            {
                _logger.Debug($"DEP GetMetadata: Parser was unable to parse. {info.Path}");
                return Task.FromResult(result);
            }

            dto.File_path = directoryService.GetFile(info.Path);

            result = Utils.DTOToEpisode(dto);

            _logger.Debug($"DEP GetMetadata: Parsed. {dto}");

            return Task.FromResult(result);
        }

        public bool HasChanged(BaseItem item, LibraryOptions libraryOptions, IDirectoryService directoryService)
        {
            _logger.Debug($"DEP HasChanged: Checking {item.Path}");

            FileSystemMetadata fileInfo = directoryService.GetFile(item.Path);
            bool result = fileInfo.Exists && fileInfo.LastWriteTimeUtc.ToUniversalTime() > item.DateLastSaved.ToUniversalTime();
            string status = result ? "Has Changed" : "Has Not Changed";

            _logger.Debug($"DEP HasChanged Result: {status}");

            return result;
        }
    }
}
