using System;
using System.Collections.Generic;
using UnityEngine;
using ScreenFlow;

namespace ScreenFlow.MVP
{
    /// <summary>
    /// Manages screens in MVP pattern. Responsible for creating View-Presenter pairs,
    /// injecting models and handling screen transitions.
    /// </summary>
    public class ScreenChanger : MonoBehaviour
    {
        [SerializeField] private ViewContainer _viewContainer;
        
        /// <summary>
        /// Dictionary to track active presenters by screen type
        /// </summary>
        private readonly Dictionary<Type, IPresenter> _activePresenters = new();
        
        /// <summary>
        /// Stack to track screen navigation history for back navigation
        /// </summary>
        private readonly Stack<Type> _screenHistory = new();

        #region Public API

        /// <summary>
        /// Opens a screen with the specified model data. Creates View-Presenter pair if not exists.
        /// </summary>
        /// <typeparam name="TView">Type of view to display</typeparam>
        /// <typeparam name="TModel">Type of model data</typeparam>
        /// <typeparam name="TPresenter">Type of presenter to create</typeparam>
        /// <param name="model">Model data to pass to presenter</param>
        /// <param name="hideOthers">Whether to hide all other screens when showing this one</param>
        /// <returns>The created or existing presenter instance</returns>
        public TPresenter ShowScreen<TView, TModel, TPresenter>(TModel model, bool hideOthers = true)
            where TView : View, IView<TModel>
            where TPresenter : class, IPresenter<TView, TModel>, new()
        {
            if (hideOthers)
            {
                HideAllScreens();
            }

            var view = _viewContainer.GetOrCreateView<TView>();
            var presenter = GetOrCreatePresenter<TView, TModel, TPresenter>(view);
            
            presenter.Initialize(model);
            view.Show();
            
            TrackScreenInHistory<TView>();

            return presenter;
        }

        /// <summary>
        /// Shows a screen without model data (for screens that don't require data)
        /// </summary>
        /// <typeparam name="TView">Type of view to display</typeparam>
        /// <typeparam name="TPresenter">Type of presenter to create</typeparam>
        /// <param name="hideOthers">Whether to hide all other screens when showing this one</param>
        /// <returns>The created or existing presenter instance</returns>
        public TPresenter ShowScreen<TView, TPresenter>(bool hideOthers = true)
            where TView : View
            where TPresenter : class, IPresenter<TView>, new()
        {
            if (hideOthers)
            {
                HideAllScreens();
            }

            var view = _viewContainer.GetOrCreateView<TView>();
            var presenter = GetOrCreatePresenter<TView, TPresenter>(view);
            
            presenter.Initialize();
            view.Show();
            
            TrackScreenInHistory<TView>();

            return presenter;
        }

        /// <summary>
        /// Hides a specific screen and destroys its presenter
        /// </summary>
        /// <typeparam name="TView">Type of view to hide</typeparam>
        public void HideScreen<TView>() where TView : View
        {
            var viewType = typeof(TView);
            
            if (_activePresenters.TryGetValue(viewType, out var presenter))
            {
                presenter.Dispose();
                _activePresenters.Remove(viewType);
            }

            var view = _viewContainer.GetOrCreateView<TView>();
            view.Hide();
        }

        /// <summary>
        /// Hides all active screens
        /// </summary>
        public void HideAllScreens()
        {
            _viewContainer.HideAllViews();
            
            foreach (var presenter in _activePresenters.Values)
            {
                presenter?.Dispose();
            }
            _activePresenters.Clear();
        }

        /// <summary>
        /// Navigates back to the previous screen in history
        /// </summary>
        /// <returns>True if navigation was successful, false if no history exists</returns>
        public bool GoBack()
        {
            if (_screenHistory.Count <= 1) // Current screen is always on top
                return false;

            // Remove current screen from history
            _screenHistory.Pop();
            
            if (_screenHistory.Count > 0)
            {
                var previousScreenType = _screenHistory.Peek();
                ShowScreenByType(previousScreenType);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the currently active presenter of specified type
        /// </summary>
        /// <typeparam name="TPresenter">Type of presenter to retrieve</typeparam>
        /// <returns>Active presenter instance or null if not found</returns>
        public TPresenter GetActivePresenter<TPresenter>() where TPresenter : class, IPresenter
        {
            foreach (var presenter in _activePresenters.Values)
            {
                if (presenter is TPresenter typedPresenter)
                    return typedPresenter;
            }
            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets existing presenter or creates a new one for the specified view and model types
        /// </summary>
        private TPresenter GetOrCreatePresenter<TView, TModel, TPresenter>(TView view)
            where TView : View, IView<TModel>
            where TPresenter : class, IPresenter<TView, TModel>, new()
        {
            var viewType = typeof(TView);
            
            if (_activePresenters.TryGetValue(viewType, out var existingPresenter))
            {
                return (TPresenter)existingPresenter;
            }

            var newPresenter = new TPresenter();
            newPresenter.SetView(view);
            _activePresenters[viewType] = newPresenter;
            
            return newPresenter;
        }

        /// <summary>
        /// Gets existing presenter or creates a new one for the specified view type (without model)
        /// </summary>
        private TPresenter GetOrCreatePresenter<TView, TPresenter>(TView view)
            where TView : View
            where TPresenter : class, IPresenter<TView>, new()
        {
            var viewType = typeof(TView);
            
            if (_activePresenters.TryGetValue(viewType, out var existingPresenter))
            {
                return (TPresenter)existingPresenter;
            }

            var newPresenter = new TPresenter();
            newPresenter.SetView(view);
            _activePresenters[viewType] = newPresenter;
            
            return newPresenter;
        }

        /// <summary>
        /// Tracks screen in navigation history
        /// </summary>
        private void TrackScreenInHistory<TView>()
        {
            var viewType = typeof(TView);
            
            // Remove from history if already exists (avoid duplicates)
            if (_screenHistory.Contains(viewType))
            {
                var tempStack = new Stack<Type>();
                while (_screenHistory.Count > 0)
                {
                    var type = _screenHistory.Pop();
                    if (type != viewType)
                        tempStack.Push(type);
                }
                
                while (tempStack.Count > 0)
                {
                    _screenHistory.Push(tempStack.Pop());
                }
            }
            
            _screenHistory.Push(viewType);
        }

        /// <summary>
        /// Shows screen by its type (used for back navigation)
        /// </summary>
        private void ShowScreenByType(Type screenType)
        {
            // This is a simplified implementation
            // In a real project, you'd want to store more information about how to recreate the screen
            Debug.LogWarning($"Back navigation to {screenType.Name} - implement screen recreation logic based on your needs");
        }

        #endregion

        #region Unity Lifecycle

        private void Awake()
        {
            if (_viewContainer == null)
            {
                _viewContainer = FindObjectOfType<ViewContainer>();
                if (_viewContainer == null)
                {
                    Debug.LogError("ViewContainer not found! Please assign it in the inspector or ensure it exists in the scene.");
                }
            }
        }

        private void OnDestroy()
        {
            HideAllScreens(); // Clean up all presenters
        }

        #endregion
    }
}