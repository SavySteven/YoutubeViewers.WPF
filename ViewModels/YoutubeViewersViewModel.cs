using System.Windows.Input;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YouTubeViewersViewModel : ViewModelBase
    {

        public YouTubeViewersListingItemViewModel YoutubeViewersListingItemViewModel { get; }
        public YoutubeViewerDetailsFormViewModel YoutubeViewerDetailsFormViewModel { get; }


        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand LoadYouTubeViewersCommand { get; }
        public ICommand AddYouTubeViewersCommand { get; }

        public YouTubeViewersViewModel(YoutubeViewersStore youTubeViewersStore, SelectedYouTubeViewerStore selectedYouTubeViewerStore, ModalNavigationStore modalNavigationStore)
        {
            YouTubeViewersListingViewModel = new YouTubeViewersListingViewModel(youTubeViewersStore, selectedYouTubeViewerStore, modalNavigationStore);
            YouTubeViewersDetailsViewModel = new YouTubeViewersDetailsViewModel(selectedYouTubeViewerStore);

            LoadYouTubeViewersCommand = new LoadYouTubeViewersCommand(this, youTubeViewersStore);
            AddYouTubeViewersCommand = new OpenAddYoutubeViewerCommand(youTubeViewersStore, modalNavigationStore);
        }

        public static YouTubeViewersViewModel LoadViewModel(YoutubeViewersStore youTubeViewersStore, SelectedYouTubeViewerStore selectedYouTubeViewerStore, ModalNavigationStore modalNavigationStore)
        {
            YouTubeViewersViewModel viewModel = new YouTubeViewersViewModel(youTubeViewersStore, selectedYouTubeViewerStore, modalNavigationStore);

            viewModel.LoadYouTubeViewersCommand.Execute(null);

            return viewModel;
        }
    }
}
