namespace _ScreenFlow.Runtime.Scripts.New
{
    public class NavigateToScreenCommand<T> : NavigationCommand where T : IPresenter
    {
        private readonly object _parameters;
    
        public NavigateToScreenCommand(object parameters = null)
        {
            _parameters = parameters;
        }
    
        public override void Execute(INavigationService navigation)
        {
            if (_parameters != null)
                navigation.NavigateTo<T>(_parameters);
            else
                navigation.NavigateTo<T>();
        }
    }
}