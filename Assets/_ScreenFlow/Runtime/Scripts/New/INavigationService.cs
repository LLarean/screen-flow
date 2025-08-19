namespace _ScreenFlow.Runtime.Scripts.New
{
    public interface INavigationService
    {
        void NavigateTo<T>() where T : IPresenter;
        void NavigateTo<T>(object parameters) where T : IPresenter;
        void GoBack();
        void CloseScreen<T>() where T : IPresenter;
    }
}