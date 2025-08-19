namespace _ScreenFlow.Runtime.Scripts.New
{
    public class MainMenuPresenter : BasePresenter
    {
        private readonly MainMenuView _view;
        private readonly IGameDataModel _model;
    
        public MainMenuPresenter(MainMenuView view, IGameDataModel model, INavigationService navigation) 
            : base(navigation)
        {
            _view = view;
            _model = model;
        
            _view.OnPlayButtonClicked += OnPlayButtonClicked;
            _view.OnSettingsButtonClicked += OnSettingsButtonClicked;
        }
    
        private void OnPlayButtonClicked()
        {
            var playerData = _model.GetPlayerData();
        
            // var command = new NavigateToScreenCommand<GameplayPresenter>(playerData);
            // ExecuteNavigation(command);
        }
    
        private void OnSettingsButtonClicked()
        {
            // ExecuteNavigation(new NavigateToScreenCommand<SettingsPresenter>());
        }
    }
}