using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class OpenAddYoutubeViewerCommand : CommandBase
    {
        private readonly YouTubeViewersStore _youTubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddYoutubeViewerCommand(YoutubeViewersStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _youTubeViewersStore = youTubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddYoutubeViewerViewModel addYouTubeViewerViewModel = new AddYoutubeViewerViewModel(_youTubeViewersStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addYouTubeViewerViewModel;
        }
    }
}
