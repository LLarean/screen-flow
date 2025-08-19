using System;
using System.Collections.Generic;

namespace _ScreenFlow.Runtime.Scripts.New
{
    public class NavigationService : INavigationService
    {
        private readonly IScreenFactory _screenFactory;
        private readonly Dictionary<Type, IPresenter> _activeScreens;
        private readonly Stack<Type> _navigationHistory;
    
        public NavigationService(IScreenFactory screenFactory)
        {
            _screenFactory = screenFactory;
            _activeScreens = new Dictionary<Type, IPresenter>();
            _navigationHistory = new Stack<Type>();
        }
    
        public void NavigateTo<T>() where T : IPresenter
        {
            NavigateTo<T>(null);
        }
    
        public void NavigateTo<T>(object parameters) where T : IPresenter
        {
            var screenType = typeof(T);
        
            if (_activeScreens.TryGetValue(screenType, out var existingPresenter))
            {
                ShowScreen(existingPresenter);
                return;
            }
        
            var presenter = _screenFactory.CreatePresenter<T>(parameters);
            _activeScreens[screenType] = presenter;
            _navigationHistory.Push(screenType);
        
            ShowScreen(presenter);
        }
    
        public void GoBack()
        {
            if (_navigationHistory.Count > 1)
            {
                _navigationHistory.Pop();
                var previousScreenType = _navigationHistory.Peek();
            
                if (_activeScreens.TryGetValue(previousScreenType, out var presenter))
                {
                    ShowScreen(presenter);
                }
            }
        }
    
        public void CloseScreen<T>() where T : IPresenter
        {
            var screenType = typeof(T);
            if (_activeScreens.TryGetValue(screenType, out var presenter))
            {
                HideScreen(presenter);
                _activeScreens.Remove(screenType);
            }
        }
    
        private void ShowScreen(IPresenter presenter)
        {
            foreach (var activePresenter in _activeScreens.Values)
            {
                if (activePresenter != presenter)
                    HideScreen(activePresenter);
            }
        
            presenter.Show();
        }
    
        private void HideScreen(IPresenter presenter)
        {
            presenter.Hide();
        }
    }
}