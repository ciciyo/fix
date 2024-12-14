using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlexusTechdev.Utility
{
    [System.Serializable]
    public struct TextStyleData
    {
        public TMP_FontAsset FontAsset; // Font asset dari TextMeshPro
        public float FontSize; // Ukuran font
        public Color FontColor; // Warna font
        public TextAlignmentOptions Alignment; // Perataan teks
        
    }

    public class Button_tab : MonoBehaviour
    {
        public TabManager tabManager;
        public int id;
        public Button button;

        [Header("Image Color")]
        public Image image;
        public Color color_selected = Color.white;
        public Color color_unselected = Color.grey;

        [Header("Text")]
        public TMP_Text text;
        public Color colorText_selected = Color.grey;
        public Color colorText_unselected = Color.white;

        public bool updateByStyle = false;
        [SerializeField] private TextStyleData style_selected; // Data gaya teks
        [SerializeField] private TextStyleData style_unselected; // Data gaya teks

        private void Start()
        {
            button.onClick.AddListener(Select);
        }

        public void Select()
        {
            UpdateCustomUI(true);
            tabManager.SelectButton(this);
        }

        public void UnSelected()
        {
            UpdateCustomUI(false);
        }


        public void UpdateCustomUI(bool selected)
        {
            if (selected)
            {
                if (image != null) image.color = color_selected;
                if (text != null)
                {

                    if (updateByStyle)
                    {
                        text.font = style_selected.FontAsset;
                        text.color = style_selected.FontColor;
                        text.alignment = style_selected.Alignment;
                        text.fontSize = style_selected.FontSize;
                    }
                    else
                    {
                        text.color = colorText_selected;
                    }

                    
                     
                }
            }
            else
            {
                if (image != null) image.color = color_unselected;
                if (text != null)
                {
                    

                    if (updateByStyle)
                    {
                        text.font = style_unselected.FontAsset;
                        text.color = style_unselected.FontColor;
                        text.alignment = style_unselected.Alignment;
                        text.fontSize = style_unselected.FontSize;
                    }
                    else
                    {
                        text.color = colorText_unselected;
                    }
                }
            }
        }

        }
}