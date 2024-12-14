using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    [CreateAssetMenu(menuName = "Configuration/GameData")]
    public class GameData : ScriptableObject
    {
        public CharacterTypeLibrary characterTypeLibrary;



    }

    [Serializable]
    public class CharacterTypeLibrary
    {
        public List<CharacterTypeData> datas;

        public Sprite GetCharacterTypeSprite(CharacterType type)
        {
            CharacterTypeData data = datas.Find(types => types.type == type);

            return data.sprite;
        }
    }

    [Serializable]
    public class CharacterTypeData
    {
        public CharacterType type;
        public Sprite sprite;
    }

}