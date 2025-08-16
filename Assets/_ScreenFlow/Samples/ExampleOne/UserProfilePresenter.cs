namespace ScreenFlow.MVP
{
    /// <summary>
    /// Example presenter for user profile screen
    /// </summary>
    public class UserProfilePresenter : IPresenter<UserProfileView, UserProfileModel>
    {
        private UserProfileView _view;
        private UserProfileModel _model;

        public void SetView(UserProfileView view)
        {
            _view = view;
        }

        public void Initialize(UserProfileModel model)
        {
            _model = model;
            _view.UpdateView(_model);
            
            // Subscribe to view events if needed
            // _view.OnBackButtonClicked += HandleBackButton;
        }

        public void Dispose()
        {
            // Unsubscribe from events
            // _view.OnBackButtonClicked -= HandleBackButton;
            _view = null;
            _model = null;
        }
    }
}