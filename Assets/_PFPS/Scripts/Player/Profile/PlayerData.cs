using HeavenFalls;
using HeavenFalls.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public Profile profile;
    public CharacterData characterData;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


}


#region Class and Struct
[System.Serializable]
public struct Profile
{
    public int id;
    public string username;
    public string email;
    public int level;
    public Sprite avatar;

    [Header("currency")]
    public int coin;
    public int diamond;

    public Profile(string username, string email) : this()
    {
        this.username = username;
        this.email = email;
    }
}

public class CharacterData: Item
{
    public int level;
    ConfigStats stats;


    public void UpgradeLevel()
    {

    }
}

#endregion
