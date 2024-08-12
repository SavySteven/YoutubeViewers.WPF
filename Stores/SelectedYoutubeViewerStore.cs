namespace YoutubeViewers.WPF.Models
{

    public class SelectedYouTubeViewerStore
    {
        private readonly YoutubeViewersStore _youTubeViewersStore;

        private YoutubeViewer _selectedYouTubeViewer;
        public YoutubeViewer SelectedYouTubeViewer
        {
            get
            {
                return _selectedYouTubeViewer;
            }
            set
            {
                _selectedYouTubeViewer = value;
                SelectedYouTubeViewerChanged?.Invoke();
            }
        }

        public event Action SelectedYouTubeViewerChanged;

        public SelectedYouTubeViewerStore(YoutubeViewersStore youTubeViewersStore)
        {
            _youTubeViewersStore = youTubeViewersStore;

            _youTubeViewersStore.YouTubeViewerAdded += YouTubeViewersStore_YouTubeViewerAdded;
            _youTubeViewersStore.YouTubeViewerUpdated += YouTubeViewersStore_YouTubeViewerUpdated;
        }

        private void YouTubeViewersStore_YouTubeViewerAdded(YoutubeViewer youTubeViewer)
        {
            SelectedYouTubeViewer = youTubeViewer;
        }

        private void YouTubeViewersStore_YouTubeViewerUpdated(YoutubeViewer youTubeViewer)
        {
            if (youTubeViewer.Id == SelectedYouTubeViewer?.Id)
            {
                SelectedYouTubeViewer = youTubeViewer;
            }
        }
    }
}

