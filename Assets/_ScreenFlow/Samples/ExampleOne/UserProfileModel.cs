using System;

namespace ScreenFlow.MVP
{
    /// <summary>
    /// Example model for user profile data
    /// </summary>
    [Serializable]
    public class UserProfileModel
    {
        public string UserName { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public string AvatarUrl { get; set; }
    }
}