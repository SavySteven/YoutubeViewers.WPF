using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class LoadYouTubeViewersCommand : AsyncCommandBase
    {
        private readonly YouTubeViewersViewModel _youTubeViewersViewModel;
        private readonly YoutubeViewersStore _youTubeViewersStore;

        public LoadYouTubeViewersCommand(YouTubeViewersViewModel youTubeViewersViewModel, YoutubeViewersStore youTubeViewersStore)
        {
            _youTubeViewersViewModel = youTubeViewersViewModel;
            _youTubeViewersStore = youTubeViewersStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _youTubeViewersViewModel.ErrorMessage = null;
            _youTubeViewersViewModel.IsLoading = true;

            try
            {
                await _youTubeViewersStore.Load();
            }
            catch (Exception)
            {
                _youTubeViewersViewModel.ErrorMessage = "Failed to load YouTube viewers. Please restart the application.";
            }
            finally
            {
                _youTubeViewersViewModel.IsLoading = false;
            }
        }
    }
}
