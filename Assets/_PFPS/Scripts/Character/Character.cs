using HeavenFalls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//belum terpakai
public enum CharacterType
{
    DAMAGE_DEALER, TANK, SUPPORT, HEALER
}

[System.Serializable]
public struct Character 
{
    public int id;
    public string name;
    public string description;
    public Sprite icon;
    public string characterType;
    public CharacterType type;
    public CharacterConfig config;
}
