using System;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenFlow
{
    public class MenuView : View
    {
        [SerializeField] private Button _settings;

        public event Action OnSettingsClicked;
        
        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            _settings.onClick.AddListener(() => OnSettingsClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _settings.onClick.RemoveAllListeners();
        }
    }
}
