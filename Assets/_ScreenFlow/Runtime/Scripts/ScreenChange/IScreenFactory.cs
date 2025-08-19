namespace _ScreenFlow.Runtime.Scripts.New
{
    public interface IScreenFactory
    {
        T CreatePresenter<T>() where T : IPresenter;
        T CreatePresenter<T>(object parameters) where T : IPresenter;
    }
}