using System;
using UnityEngine;

namespace _ScreenFlow.Runtime.Scripts.New
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        public event Action OnPlayButtonClicked;
        public event Action OnSettingsButtonClicked;
    }
}