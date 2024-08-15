using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class EditYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly EditYouTubeViewerViewModel _editYouTubeViewerViewModel;
        private readonly YoutubeViewersStore _youTubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditYoutubeViewerCommand(EditYouTubeViewerViewModel editYouTubeViewerViewModel, YouTubeViewersStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _editYouTubeViewerViewModel = editYouTubeViewerViewModel;
            _youTubeViewersStore = youTubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            YoutubeViewerDetailsFormViewModel formViewModel = _editYouTubeViewerViewModel.YouTubeViewerDetailsFormViewModel;
            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            YoutubeViewer youTubeViewer = new YoutubeViewer(
                _editYouTubeViewerViewModel.YouTubeViewerId,
                formViewModel.Username,
                formViewModel.IsSubscribed,
                formViewModel.IsMember);

            try
            {
                await _youTubeViewersStore.Update(youTubeViewer);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to update YouTube viewer. Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
