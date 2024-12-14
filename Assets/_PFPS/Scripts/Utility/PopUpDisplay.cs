using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlexusTechdev.Utility
{
    public class PopUpDisplay : MonoBehaviour
    {
        public GameObject panel;
        
        public Image image_ilustration;
        public TMP_Text titleTop_text;
        public TMP_Text titleBottom_text;
        public TMP_Text message_text;


        

        public void ShowPopUp(string title, string message, Sprite sprite_ilustration = null)
        {
            titleTop_text.text = title;
            titleBottom_text.text = title;
            message_text.text = message;

            if (image_ilustration)
            {
                if (sprite_ilustration) image_ilustration.sprite = sprite_ilustration;
                image_ilustration.gameObject.SetActive(sprite_ilustration);
            }
            
            panel.SetActive(true);
        }

        
    }
}

