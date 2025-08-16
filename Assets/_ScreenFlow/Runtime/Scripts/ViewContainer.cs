using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenFlow
{
    /// <summary>
    /// Manages UI views within a canvas, providing functionality to create, show, hide and manage view instances.
    /// Handles view lifecycle including instantiation from prefabs and proper UI transform setup.
    /// </summary>
    public class ViewContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<View> _viewsOnScene = new();
        [SerializeField] private List<View> _viewsPrefabs = new();

        /// <summary>
        /// Gets or creates an instance of the specified view type. If the view already exists on scene, returns the existing instance.
        /// If not, creates a new instance from the corresponding prefab.
        /// </summary>
        /// <typeparam name="T">The type of view to retrieve, must inherit from View</typeparam>
        /// <returns>Instance of the requested view type</returns>
        /// <exception cref="InvalidOperationException">Thrown when the prefab for the requested view type is not found in the prefabs list</exception>
        public T GetOrCreateView<T>() where T : View
        {
            var existingView = FindExistingViewOnScene<T>();
            if (existingView != null)
                return existingView;

            var newView = CreateViewFromPrefab<T>();
            if (newView == null)
            {
                throw new InvalidOperationException($"Failed to create view of type {typeof(T).Name}. " +
                    "Make sure the prefab is added to _viewsPrefabs list in the inspector.");
            }

            return newView;
        }

        /// <summary>
        /// Gets or creates an instance of the specified view type. Alias for GetOrCreateView for shorter syntax.
        /// </summary>
        /// <typeparam name="T">The type of view to retrieve, must inherit from View</typeparam>
        /// <returns>Instance of the requested view type</returns>
        /// <exception cref="InvalidOperationException">Thrown when the prefab for the requested view type is not found in the prefabs list</exception>
        public T Get<T>() where T : View => GetOrCreateView<T>();

        /// <summary>
        /// Hides all views currently active on the scene.
        /// </summary>
        public void HideAllViews()
        {
            foreach (var view in _viewsOnScene)
            {
                view?.Hide();
            }
        }

        /// <summary>
        /// Searches for an existing view of the specified type among views currently on scene.
        /// </summary>
        /// <typeparam name="T">The type of view to find</typeparam>
        /// <returns>The found view instance or null if not found</returns>
        private T FindExistingViewOnScene<T>() where T : View
        {
            foreach (var view in _viewsOnScene)
            {
                if (view is T targetView)
                    return targetView;
            }

            return null;
        }

        /// <summary>
        /// Creates a new view instance from the prefabs list for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of view to create</typeparam>
        /// <returns>New view instance or null if prefab not found</returns>
        private T CreateViewFromPrefab<T>() where T : View
        {
            foreach (var prefab in _viewsPrefabs)
            {
                if (prefab is T)
                {
                    var instance = InstantiateAndSetupView(prefab);
                    return instance as T;
                }
            }

            return null;
        }

        /// <summary>
        /// Instantiates a view from prefab, sets up its transform, initializes it and adds to the scene views collection.
        /// The created view is initially hidden.
        /// </summary>
        /// <param name="prefab">The view prefab to instantiate</param>
        /// <returns>The instantiated and configured view instance</returns>
        /// <exception cref="InvalidOperationException">Thrown when canvas is not assigned</exception>
        private View InstantiateAndSetupView(View prefab)
        {
            if (_canvas == null)
            {
                throw new InvalidOperationException("Canvas is not assigned to ViewContainer!");
            }

            var instance = Instantiate(prefab, _canvas.transform);
            ConfigureViewTransform(instance);
            instance.Initialize(this);
            instance.Hide();

            _viewsOnScene.Add(instance);
            return instance;
        }

        /// <summary>
        /// Configures the RectTransform of a view to fill the entire canvas.
        /// Sets up anchors, position, rotation and scale for proper full-screen display.
        /// </summary>
        /// <param name="view">The view whose transform should be configured</param>
        private void ConfigureViewTransform(View view)
        {
            var rectTransform = view.GetComponent<RectTransform>();

            // Reset transform properties
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localRotation = Quaternion.identity;

            // Set anchors to fill entire parent (canvas)
            rectTransform.anchorMin = Vector2.zero;    // Bottom-left corner
            rectTransform.anchorMax = Vector2.one;     // Top-right corner
            rectTransform.offsetMin = Vector2.zero;    // No offset from anchors
            rectTransform.offsetMax = Vector2.zero;    // No offset from anchors
        }
    }
}