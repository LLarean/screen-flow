using UnityEngine;

namespace _ScreenFlow.Runtime.Scripts.New
{
    public class NavigationBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            var screenFactory = new ScreenFactory();
            var navigationService = new NavigationService(screenFactory);
        
            ServiceLocator.Register<IScreenFactory>(screenFactory);
            ServiceLocator.Register<INavigationService>(navigationService);
        
            navigationService.NavigateTo<MainMenuPresenter>();
        }
    }
}