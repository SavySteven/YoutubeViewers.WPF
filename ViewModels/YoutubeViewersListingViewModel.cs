using System.Collections.ObjectModel;
using System.Collections.Specialized;
using YoutubeViewers.WPF.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YouTubeViewersListingViewModel : ViewModelBase
    {
        private readonly YoutubeViewersStore _youTubeViewersStore;
        private readonly SelectedYouTubeViewerStore _selectedYouTubeViewerStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly ObservableCollection<YouTubeViewersListingItemViewModel> _youtubeViewersListingItemViewModels;
        public IEnumerable<YouTubeViewersListingItemViewModel> YouTubeViewersListingItemViewModels => _youtubeViewersListingItemViewModels;

        public YouTubeViewersListingItemViewModel SelectedYouTubeViewerListingItemViewModel
        {
            get
            {
                return _youtubeViewersListingItemViewModels
                    .FirstOrDefault(y => y.YouTubeViewer?.Id == _selectedYouTubeViewerStore.SelectedYouTubeViewer?.Id);
            }
            set
            {
                _selectedYouTubeViewerStore.SelectedYouTubeViewer = value?.YouTubeViewer;
            }
        }

        public YouTubeViewersListingViewModel(YouTubeViewersStore youTubeViewersStore, SelectedYouTubeViewerStore selectedYouTubeViewerStore, ModalNavigationStore modalNavigationStore)
        {
            _youTubeViewersStore = youTubeViewersStore;
            _selectedYouTubeViewerStore = selectedYouTubeViewerStore;
            _modalNavigationStore = modalNavigationStore;
            _youtubeViewersListingItemViewModels = new ObservableCollection<YouTubeViewersListingItemViewModel>();

            _selectedYouTubeViewerStore.SelectedYouTubeViewerChanged += SelectedYouTubeViewerStore_SelectedYouTubeViewerChanged;

            _youTubeViewersStore.YouTubeViewersLoaded += YouTubeViewersStore_YouTubeViewersLoaded;
            _youTubeViewersStore.YouTubeViewerAdded += YouTubeViewersStore_YouTubeViewerAdded;
            _youTubeViewersStore.YouTubeViewerUpdated += YouTubeViewersStore_YouTubeViewerUpdated;
            _youTubeViewersStore.YouTubeViewerDeleted += YouTubeViewersStore_YouTubeViewerDeleted;

            _youtubeViewersListingItemViewModels.CollectionChanged += YouTubeViewersListingItemViewModels_CollectionChanged;
        }

        protected override void Dispose()
        {
            _selectedYouTubeViewerStore.SelectedYouTubeViewerChanged -= SelectedYouTubeViewerStore_SelectedYouTubeViewerChanged;

            _youTubeViewersStore.YouTubeViewersLoaded -= YouTubeViewersStore_YouTubeViewersLoaded;
            _youTubeViewersStore.YouTubeViewerAdded -= YouTubeViewersStore_YouTubeViewerAdded;
            _youTubeViewersStore.YouTubeViewerUpdated -= YouTubeViewersStore_YouTubeViewerUpdated;
            _youTubeViewersStore.YouTubeViewerDeleted -= YouTubeViewersStore_YouTubeViewerDeleted;

            base.Dispose();
        }

        private void SelectedYouTubeViewerStore_SelectedYouTubeViewerChanged()
        {
            OnPropertyChanged(nameof(SelectedYouTubeViewerListingItemViewModel));
        }

        private void YouTubeViewersStore_YouTubeViewersLoaded()
        {
            _youTubeViewersListingItemViewModels.Clear();

            foreach (YouTubeViewer youTubeViewer in _youTubeViewersStore.YouTubeViewers)
            {
                AddYouTubeViewer(youTubeViewer);
            }
        }

        private void YouTubeViewersStore_YouTubeViewerAdded(YouTubeViewer youTubeViewer)
        {
            AddYouTubeViewer(youTubeViewer);
        }

        private void YouTubeViewersStore_YouTubeViewerUpdated(YouTubeViewer youTubeViewer)
        {
            YouTubeViewersListingItemViewModel youTubeViewerViewModel =
                _youTubeViewersListingItemViewModels.FirstOrDefault(y => y.YouTubeViewer.Id == youTubeViewer.Id);

            if (youTubeViewerViewModel != null)
            {
                youTubeViewerViewModel.Update(youTubeViewer);
            }
        }

        private void YouTubeViewersStore_YouTubeViewerDeleted(Guid id)
        {
            YouTubeViewersListingItemViewModel itemViewModel = _youTubeViewersListingItemViewModels.FirstOrDefault(y => y.YouTubeViewer?.Id == id);

            if (itemViewModel != null)
            {
                _youTubeViewersListingItemViewModels.Remove(itemViewModel);
            }
        }

        private void YouTubeViewersListingItemViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedYouTubeViewerListingItemViewModel));
        }

        private void AddYouTubeViewer(YouTubeViewer youTubeViewer)
        {
            YouTubeViewersListingItemViewModel itemViewModel =
                new YouTubeViewersListingItemViewModel(youTubeViewer, _youTubeViewersStore, _modalNavigationStore);
            _youTubeViewersListingItemViewModels.Add(itemViewModel);
        }
    }
}
