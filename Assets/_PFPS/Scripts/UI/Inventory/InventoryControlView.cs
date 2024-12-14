using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HeavenFalls.Inventory
{
    public class InventoryControlView : MonoBehaviour
    {
        [SerializeField] private List<InventoryItem> inventoryItems = new List<InventoryItem>();
        public Transform slot_parent;
        public InventorySlot slot_prefab;
        public List<InventorySlot> slots;


        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            ResetData();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if(slots.Count > i)
                {
                    slots[i].data = inventoryItems[i];
                    slots[i].Initialize();
                    slots[i].gameObject.SetActive(true);
                }
                else
                {
                    InventorySlot slot = Instantiate(slot_prefab, slot_parent);
                    slot.data = inventoryItems[i];
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
}

