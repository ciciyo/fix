using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlexusTechdev.Utility
{
    //[ExecuteInEditMode]
    public class ImageFillController : MonoBehaviour
    {
        public Image background;
        public Image image;
        [Range(0,1)] public float value;

        [Header("Fill Positive")]
        public Sprite sprite_background;
        public Sprite sprite_normal;
        public Sprite sprite_full;

        [Header("Fill Negative")]
        public Sprite sprite_background_negatif;
        public Sprite sprite_negatif;
        public Sprite sprite_negatif_full;

        public void Initialize(float value)
        {
            value = Mathf.Clamp(value, -1f, 1f);

            if (background != null)
            {
                background.sprite = value >= 0 ? sprite_background : sprite_background_negatif;
            }

            if (value >= 1) image.sprite = sprite_full;
            else if (value >= 0) image.sprite = sprite_normal;
            else
            {
                if (value > -1) image.sprite = sprite_negatif;
                else image.sprite = sprite_negatif_full;

            }


            value = Mathf.Abs(value);
            image.fillAmount = value;
        }

        public void Reset()
        {
            Initialize(0f);
        }
    }
}