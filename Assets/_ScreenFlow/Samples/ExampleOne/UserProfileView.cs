using UnityEngine;

namespace ScreenFlow.MVP
{
    /// <summary>
    /// Example view for user profile screen
    /// </summary>
    public class UserProfileView : View, IView<UserProfileModel>
    {
        [Header("UI Components")]
        [SerializeField] private TMPro.TextMeshProUGUI _userNameText;
        [SerializeField] private TMPro.TextMeshProUGUI _levelText;
        [SerializeField] private UnityEngine.UI.Slider _experienceSlider;

        public void UpdateView(UserProfileModel model)
        {
            if (_userNameText != null)
                _userNameText.text = model.UserName;
            
            if (_levelText != null)
                _levelText.text = $"Level {model.Level}";
            
            if (_experienceSlider != null)
                _experienceSlider.value = model.Experience / 1000f; // Assuming 1000 is max XP per level
        }

        // Example button click handlers
        public void OnBackButtonClicked()
        {
            // Presenter will handle this
        }

        public void OnSettingsButtonClicked()
        {
            // Presenter will handle this
        }
    }
}