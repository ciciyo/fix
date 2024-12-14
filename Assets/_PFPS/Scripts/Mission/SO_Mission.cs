using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Map and Mission")]
public class SO_Mission : ScriptableObject
{
    public SO_Mission so_default;

    public List<Map> maps;



    /// <summary>
    /// merupakan inisiasi ketika suatu akun baru akan memulai progress dan bisa digunakan saat player menggunakan mode guest (tidak terhubung internet)/main solo
    /// </summary>
    [ContextMenu("Reset Data To Default")]
    public void ResetDataToDefault()
    {
        if (so_default == null)
        {
            Debug.Log("The 'so_default' field must not be empty.");
            return;
        }

        maps = new List<Map>();
        for (int i = 0; i < so_default.maps.Count; i++)
        {
            maps.Add(so_default.maps[i].GetClone());
        }
    }

    [ContextMenu("Lock all mission")]
    public void LockAllMission()
    {
        for (int i = 0; i < maps.Count; i++)
        {
            for (int j = 0; j < maps[i].missions.Count; j++)
            {
                maps[i].missions[j].isLocked = true;
            }
        }
    }

    [ContextMenu("Unlock all mission")]
    public void UnlockAllMission()
    {
        for (int i = 0; i < maps.Count; i++)
        {
            for (int j = 0; j < maps[i].missions.Count; j++)
            {
                maps[i].missions[j].isLocked = false;
            }
        }
    }



    public Map GetMapById(int id) => maps[id-1];


}
