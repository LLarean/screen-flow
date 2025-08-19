using System;
using System.Collections.Generic;

namespace _ScreenFlow.Runtime.Scripts.New
{
    public class ScreenFactory : IScreenFactory
    {
        private readonly Dictionary<Type, Func<object, IPresenter>> _creators;

        public ScreenFactory()
        {
            _creators = new Dictionary<Type, Func<object, IPresenter>>();
            RegisterCreators();
        }

        private void RegisterCreators()
        {
            // _creators[typeof(MainMenuPresenter)] = (params_) =>
            // {
            //     var view = Object.FindObjectOfType<MainMenuView>();
            //     
            //     var model = new GameDataModel();
            //     return new MainMenuPresenter(view, model, ServiceLocator.Get<INavigationService>());
            // };
            //
            // _creators[typeof(GameplayPresenter)] = (params_) =>
            // {
            //     var view = Object.FindObjectOfType<GameplayView>();
            //     var model = new GameplayModel();
            //     return new GameplayPresenter(view, model, ServiceLocator.Get<INavigationService>(), params_);
            // };
        }

        public T CreatePresenter<T>() where T : IPresenter
        {
            return CreatePresenter<T>(null);
        }

        public T CreatePresenter<T>(object parameters) where T : IPresenter
        {
            var type = typeof(T);
            if (_creators.TryGetValue(type, out var creator))
            {
                return (T)creator(parameters);
            }

            throw new InvalidOperationException($"No creator registered for {type.Name}");
        }
    }
}