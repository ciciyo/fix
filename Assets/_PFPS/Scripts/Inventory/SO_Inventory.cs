
using HeavenFalls.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Configuration/Inventory_Master_Data")]
public class SO_Inventory : ScriptableObject
{
    public InventoryData inventoryData;

    
}


[System.Serializable]
public struct InventoryData
{
    public List<InventoryItem> inventories;
    
    public InventoryItem GetRandomInventory()
    {
        if(inventories.Count <= 0) return null;

        int rand = Random.Range(0, inventories.Count);
        return inventories[rand];
    }
}

