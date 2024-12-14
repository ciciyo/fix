using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlexusTechdev.Utility.ScreenManagement
{
    public class ScreenManager : MonoBehaviour
    {
        public List<Screen> screens;

        public Screen default_openScreen;
        public bool closeAllScreenOnStart;


        private void Start()
        {
            if(screens.Count == 0) GetAllScreenInChild();
            if (closeAllScreenOnStart) CloseAllScreen(); 
            if (default_openScreen != null) OpenScreen(default_openScreen.screenName);

        }

        [ContextMenu("GetAllScreenInChild")]
        private void GetAllScreenInChild()
        {
            screens = new List<Screen>();
            for(int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out Screen screen))
                {
                    screens.Add(screen);
                }
            }
        }


        public Screen GetScreenByName(string screenName) => screens.Find(x => x.screenName == screenName);
        public void OpenScreen(string screenName, bool closeOtherScreen = false)
        {
            if (closeOtherScreen)
            {
                for (int i = 0; i < screens.Count; i++)
                {
                    screens[i].gameObject.SetActive(screens[i].screenName == screenName);
                }
            }
            else
            {
                Screen screen = GetScreenByName(screenName);
                if(screen != null) screen.gameObject.SetActive(true);
            }
        }

        public void CloseScreen(string screenName)
        {
            Screen screen = GetScreenByName(screenName);
            if (screen != null) screen.gameObject.SetActive(false);
        }

        public void CloseAllScreen()
        {
            for (int i = 0; i < screens.Count; i++)
            {
                screens[i].gameObject.SetActive(false);
            }
        }

    }
}