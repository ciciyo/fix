using HeavenFalls.Inventory;
using HeavenFalls.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : ItemSlot
{
    [Header("Data")]
    public InventoryItem data;
    public TMP_Text amountText;
    public Image img_icon;

    public override void Initialize()
    {
        nameText.text = data.name;
        amountText.text = $"{data.amount:N0}";
        img_icon.sprite = data.icon;
    }

    public override void ResetData()
    {
        gameObject.SetActive(false);
    }

    
}
