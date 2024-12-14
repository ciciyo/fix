using HeavenFalls.Inventory;
using HeavenFalls.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionSlot : ItemSlot
{

    public UnityAction<int> onSelect;
    public Mission data;

    [SerializeField] private bool showMissionType;
    
    public Image markup_new;
    public Image icon;
    [Header("Sprites")]
    public Sprite sprite_locked;
    public Sprite sprite_unlocked;
    public Sprite sprite_complete;

    public void Select()
    {
        Debug.Log("select");
        onSelect?.Invoke(data.id);
    }

    public override void Initialize()
    {
        if (data.isLocked) icon.sprite = sprite_locked;
        else
        {
            icon.sprite = data.isComplete ? sprite_complete : sprite_unlocked;
        }
        
    }

    public override void ResetData()
    {
        throw new System.NotImplementedException();
    }
}
