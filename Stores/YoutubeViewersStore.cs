using YoutubeViewers.Domain.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.Domain.Queries;

namespace YoutubeViewers.WPF.Stores
{
    public class YoutubeViewersStore
    {
        private readonly IGetAllYouTubeViewersQuery _getAllYouTubeViewersQuery;
        private readonly ICreateYouTubeViewerCommand _createYouTubeViewerCommand;
        private readonly IUpdateYouTubeViewerCommand _updateYouTubeViewerCommand;
        private readonly IDeleteYouTubeViewerCommand _deleteYouTubeViewerCommand;

        private readonly List<YoutubeViewer> _youTubeViewers;
        public IEnumerable<YoutubeViewer> YouTubeViewers => _youTubeViewers;

        public event Action YouTubeViewersLoaded;
        public event Action<YoutubeViewer> YouTubeViewerAdded;
        public event Action<YoutubeViewer> YouTubeViewerUpdated;
        public event Action<Guid> YouTubeViewerDeleted;

        public YoutubeViewersStore(IGetAllYouTubeViewersQuery getAllYouTubeViewersQuery,
            ICreateYouTubeViewerCommand createYouTubeViewerCommand,
            IUpdateYouTubeViewerCommand updateYouTubeViewerCommand,
            IDeleteYouTubeViewerCommand deleteYouTubeViewerCommand)
        {
            _getAllYouTubeViewersQuery = getAllYouTubeViewersQuery;
            _createYouTubeViewerCommand = createYouTubeViewerCommand;
            _updateYouTubeViewerCommand = updateYouTubeViewerCommand;
            _deleteYouTubeViewerCommand = deleteYouTubeViewerCommand;

            _youTubeViewers = new List<YoutubeViewer>();
        }

        public async Task Load()
        {
            IEnumerable<YoutubeViewer> youTubeViewers = await _getAllYouTubeViewersQuery.Execute();

            _youTubeViewers.Clear();
            _youTubeViewers.AddRange(youTubeViewers);

            YouTubeViewersLoaded?.Invoke();
        }

        public async Task Add(YoutubeViewer youTubeViewer)
        {
            await _createYouTubeViewerCommand.Execute(youTubeViewer);

            _youTubeViewers.Add(youTubeViewer);

            YouTubeViewerAdded?.Invoke(youTubeViewer);
        }

        public async Task Update(YoutubeViewer youTubeViewer)
        {
            await _updateYouTubeViewerCommand.Execute(youTubeViewer);

            int currentIndex = _youTubeViewers.FindIndex(y => y.Id == youTubeViewer.Id);

            if (currentIndex != -1)
            {
                _youTubeViewers[currentIndex] = youTubeViewer;
            }
            else
            {
                _youTubeViewers.Add(youTubeViewer);
            }

            YouTubeViewerUpdated?.Invoke(youTubeViewer);
        }

        public async Task Delete(Guid id)
        {
            await _deleteYouTubeViewerCommand.Execute(id);

            _youTubeViewers.RemoveAll(y => y.Id == id);

            YouTubeViewerDeleted?.Invoke(id);
        }
    }
}
