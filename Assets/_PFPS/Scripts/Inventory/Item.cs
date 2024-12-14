using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls.Inventory
{
    public enum InventoryType
    {
        Currency = 0, Weapon = 1, 
    }

    [System.Serializable]
    public class Item
    {
        public int id;
        public string name;
        [TextArea(3,5)]
        public string description;
    }


    [System.Serializable]
    public class InventoryItem : Item
    {
        public int inv_id;
        public int amount;
        public Sprite icon;
        public InventoryType inventoryType;

        public InventoryItem(int id,  string name, string description, int amount, InventoryType inventoryType)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.amount = amount;
            this.inventoryType = inventoryType;
        }

        public InventoryItem GetClone()
        {
            InventoryItem reward = new InventoryItem(id, name, description, amount, inventoryType);
            reward.inv_id = inv_id;
            reward.icon = icon;
            return reward;
        }
    }
}