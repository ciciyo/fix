using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Character Data")]
public class SO_CharacterData : ScriptableObject
{
    public List<Character> characters;

    public Character GetCharacterById(int id)=> characters.Find(x => x.id == id);

    public const float MAX_STATEVALUE = 2500;
}
