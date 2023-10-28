using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.IO;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using DailyExtender.Helpers;
using MediaBrowser.Controller.Entities.TV;

namespace DailyExtender.Provider
{
    public abstract class LocalEpisodeProvider : ILocalMetadataProvider<Episode>
    {
        protected readonly IFileSystem _fileSystem;

        /// <summary>
        /// Providers name, this appears in the library metadata settings.
        /// </summary>
        public abstract string Name { get; }

        public LocalEpisodeProvider(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Retrieves metadata of item.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="directoryService"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<MetadataResult<Episode>> GetMetadata(ItemInfo info, IDirectoryService directoryService, CancellationToken cancellationToken)
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
