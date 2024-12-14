using HeavenFalls.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardControlView : MonoBehaviour
{
    public SO_Inventory SO_Inventory_master;
    public int reward_count = 5;
    public int min_amount = 2;
    public int max_amount = 10;

    public List<InventoryItem> rewards = new List<InventoryItem>();
    public Transform slot_parent;
    public InventorySlot slot_prefab;
    public List<InventorySlot> slots;



    public void GetRandomReward()
    {
        rewards = new List<InventoryItem>();
        int max_reward = Mathf.Min(reward_count, SO_Inventory_master.inventoryData.inventories.Count);
        for (int i = 0; i < max_reward; i++)
        {
            InventoryItem item = SO_Inventory_master.inventoryData.GetRandomInventory();
            InventoryItem old_item = rewards.Find(data => data.id == item.id);
            if(old_item != null)
            {
                i--;
            }
            else
            {
                item.amount = Random.Range(min_amount, max_amount);
                rewards.Add(item);
            }
        }

    }

    public void Initialize()
    {
        //GetRandomReward();
        ResetData();
        for (int i = 0; i < rewards.Count; i++)
        {
            if (slots.Count > i)
            {
                slots[i].data = rewards[i];
                slots[i].Initialize();
                slots[i].gameObject.SetActive(true);
            }
            else
            {
                InventorySlot slot = Instantiate(slot_prefab, slot_parent);
                slot.data = rewards[i];
                slot.Initialize();
                slots.Add(slot);
            }
        }
    }

    private void ResetData()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }

}
