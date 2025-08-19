namespace _ScreenFlow.Runtime.Scripts.New
{
    public abstract class BasePresenter : IPresenter
    {
        protected readonly INavigationService _navigation;
    
        protected BasePresenter(INavigationService navigation)
        {
            _navigation = navigation;
        }
    
        protected void ExecuteNavigation(NavigationCommand command)
        {
            command.Execute(_navigation);
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }

        public void Hide()
        {
            throw new System.NotImplementedException();
        }
    }
}