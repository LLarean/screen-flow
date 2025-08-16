using UnityEngine;

namespace ScreenFlow
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup _canvasGroup;
        
        protected ViewContainer _viewContainer;
        public bool IsVisible { get; private set; }

        public virtual void Initialize(ViewContainer viewContainer)
        {
            _viewContainer = viewContainer;
            
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
                
            OnInitialize();
        }

        public virtual void Show()
        {
            if (IsVisible) return;
            
            gameObject.SetActive(true);
            
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            }
            
            IsVisible = true;
            OnShow();
        }

        public virtual void Hide()
        {
            if (!IsVisible) return;
            
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 0f;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
            
            IsVisible = false;
            OnHide();
        }

        public virtual void Close()
        {
            Hide();
            gameObject.SetActive(false);
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
    }
}