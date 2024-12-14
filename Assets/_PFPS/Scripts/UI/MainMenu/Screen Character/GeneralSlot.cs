using HeavenFalls.Inventory;
using HeavenFalls.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneralSlot : ItemSlot
{
    
    public Item data;
    public UnityAction<int> onSelect;


    public void Select()
    {
        onSelect?.Invoke(data.id);
    }

    public override void Initialize()
    {
        nameText.text = data.name;
    }

    public override void ResetData()
    {
        throw new System.NotImplementedException();
    }
}
