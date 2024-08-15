using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class AddYouTubeViewerCommand : AsyncCommandBase
    {
        private readonly AddYoutubeViewerViewModel _addYouTubeViewerViewModel;
        private readonly YoutubeViewersStore _youTubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddYouTubeViewerCommand(AddYoutubeViewerViewModel addYouTubeViewerViewModel, YoutubeViewersStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _addYouTubeViewerViewModel = addYouTubeViewerViewModel;
            _youTubeViewersStore = youTubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            YoutubeViewerDetailsFormViewModel formViewModel = _addYouTubeViewerViewModel.YouTubeViewerDetailsFormViewModel;
            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            YoutubeViewer youTubeViewer = new YouTubeViewer(
                Guid.NewGuid(),
                formViewModel.Username,
                formViewModel.IsSubscribed,
                formViewModel.IsMember);

            try
            {
                await _youTubeViewersStore.Add(youTubeViewer);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to add YouTube viewer. Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
