using System;

namespace ScreenFlow.MVP
{
    public interface IUserProfileView
    {
        event Action OnToMenuClicked;
    
        void UpdateView(UserProfileModel model);
    }
}