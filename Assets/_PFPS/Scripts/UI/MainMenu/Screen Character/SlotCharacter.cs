using HeavenFalls.Inventory;
using HeavenFalls.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SlotCharacter : ItemSlot
{
    public Character data;

    public int id;
    public Image icon;
    public UnityAction<SlotCharacter> onSelect;

    public override void Initialize()
    {
        icon.sprite = data.icon;
    }

    public void Select()
    {
        onSelect?.Invoke(this);
    }

    public override void ResetData()
    {
        throw new System.NotImplementedException();
    }

}
