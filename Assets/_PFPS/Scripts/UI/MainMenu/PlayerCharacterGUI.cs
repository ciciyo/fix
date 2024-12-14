using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HeavenFalls
{
    [Serializable]
    public struct CharacterSelectedInfo
    {
        public string playerName;
        public string characterName;
        public Sprite icon_characterType;

        public CharacterSelectedInfo(string playerName, string characterName, Sprite icon_characterType)
        {
            this.playerName = playerName;
            this.characterName = characterName;
            this.icon_characterType = icon_characterType;
        }
    }

    public class PlayerCharacterGUI : MonoBehaviour
    {
        [HideInInspector] public CharacterSelectedInfo data; 

        public TMP_Text playerNameText;
        public TMP_Text characterNameText;
        public Image characterTypeIcon;


        public void Initialize()
        {
            playerNameText.text = data.playerName;
            characterNameText.text = data.characterName;
            characterTypeIcon.sprite = data.icon_characterType;
        }
    }
}