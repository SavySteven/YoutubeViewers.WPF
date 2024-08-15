using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class OpenEditYoutubeViewerCommand : CommandBase
    {
        private readonly YouTubeViewersListingItemViewModel _youTubeViewersListingItemViewModel;
        private readonly YoutubeViewersStore _youTubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditYoutubeViewerCommand(YouTubeViewersListingItemViewModel youTubeViewersListingItemViewModel,
            YoutubeViewersStore youTubeViewersStore,
            ModalNavigationStore modalNavigationStore)
        {
            _youTubeViewersListingItemViewModel = youTubeViewersListingItemViewModel;
            _youTubeViewersStore = youTubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            YoutubeViewer youTubeViewer = _youTubeViewersListingItemViewModel.YouTubeViewer;

            EditYouTubeViewerViewModel editYouTubeViewerViewModel =
                new EditYouTubeViewerViewModel(youTubeViewer, _youTubeViewersStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editYouTubeViewerViewModel;
        }
    }
}
