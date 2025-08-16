using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenFlow
{
    public class ViewContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<View> _viewsOnScene = new();
        [SerializeField] private List<View> _viewsPrefabs = new();
        
        public T Get<T>() where T : View
        {
            var existingView = FindViewOnScene<T>();
            if (existingView != null)
                return existingView;

            var newView = CreateView<T>();
            if (newView == null)
            {
                throw new InvalidOperationException($"Failed to create view of type {typeof(T).Name}. " +
                    "Make sure the prefab is added to _viewsPrefabs list in the inspector.");
            }
            
            return newView;
        }

        public void HideAll()
        {
            foreach (var view in _viewsOnScene)
            {
                view?.Hide();
            }
        }
        
        private T FindViewOnScene<T>() where T : View
        {
            foreach (var view in _viewsOnScene)
            {
                if (view is T targetView)
                    return targetView;
            }
            
            return null;
        }

        private T CreateView<T>() where T : View
        {
            foreach (var prefab in _viewsPrefabs)
            {
                if (prefab is T)
                {
                    var instance = InstantiateView(prefab);
                    return instance as T;
                }
            }
            
            return null;
        }
        
        private View InstantiateView(View prefab)
        {
            if (_canvas == null)
            {
                throw new InvalidOperationException("Canvas is not assigned to UIViewRegistry!");
            }
            
            var instance = Instantiate(prefab, _canvas.transform);
            SetupViewTransform(instance);
            instance.Initialize(this);
            instance.Hide();
            
            _viewsOnScene.Add(instance);
            return instance;
        }

        private void SetupViewTransform(View view)
        {
            var rectTransform = view.GetComponent<RectTransform>();
            
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localRotation = Quaternion.identity;
            
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
    }
}