using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenFlow.MVP
{
    /// <summary>
    /// Example view for user profile screen
    /// </summary>
    public class UserProfileView : View, IUserProfileView
    {
        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI _userNameText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _toMenu;

        public event Action OnToMenuClicked;

        public void UpdateView(UserProfileModel model)
        {
            _userNameText.text = model.UserName;
            _levelText.text = $"Level {model.Level}";
            _toMenu.onClick.AddListener(ToMenuClick);
        }

        private void ToMenuClick() => OnToMenuClicked?.Invoke();
    }
}