using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class AddYoutubeViewerViewModel : ViewModelBase
    {
        public YoutubeViewerDetailsFormViewModel YouTubeViewerDetailsFormViewModel { get; }

        public AddYoutubeViewerViewModel(YoutubeViewersStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand submitCommand = new AddYoutubeViewerCommand(this, youTubeViewersStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            YouTubeViewerDetailsFormViewModel = new YoutubeViewerDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
