using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HeavenFalls.UI
{
    public enum HighlightThemeType
    {
        COLOR = 0, SPRITE = 1
    }

    public abstract class ItemSlot : MonoBehaviour
    {
        public TMP_Text nameText;
        public Button button;

        [Header("Highlight")]
        public Image highlight;
        public HighlightThemeType updateHighlightBy;
        public Color color_selected = Color.white;
        public Color color_unselected;
        public Sprite sprite_selected;
        public Sprite sprite_unselected;

        public abstract void Initialize();
        public abstract void ResetData();

        public virtual void UpdateHighlight(bool isSelected)
        {
            switch (updateHighlightBy)
            {
                case HighlightThemeType.COLOR:
                    UpdateHighlightColor(isSelected);
                    break;
                case HighlightThemeType.SPRITE:
                    UpdateHighlightSprite(isSelected);
                    break;
                default:
                    break;
            }
        }

        public virtual void UpdateHighlightColor(bool isSelected)
        {
            if (!highlight) return;
            highlight.color = isSelected ? color_selected : color_unselected;
        }

        public virtual void UpdateHighlightSprite(bool isSelected)
        {
            if (!highlight || !sprite_selected || !sprite_unselected) return;
            highlight.sprite = isSelected ? sprite_selected : sprite_unselected;
        }
    }
}