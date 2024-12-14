using HeavenFalls;
using HeavenFalls.Weapon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeStatWeaponControlView : MonoBehaviour, IStatControlView
{
    [Header("stat base")]
    public TMP_Text fireRateText;
    public TMP_Text rangeText;
    public TMP_Text accuracyText;
    public TMP_Text damageText;
    public TMP_Text maxAmmoText;
    public TMP_Text reloadTimeText;

    [Header("stat upgrade preview")]
    public TMP_Text fireRate_upgradeText;
    public TMP_Text range_upgradeText;
    public TMP_Text accuracy_upgradeText;
    public TMP_Text damage_upgradeText;
    public TMP_Text maxAmmo_upgradeText;
    public TMP_Text reloadTime_upgradeText;

    [HideInInspector] public WeaponStat stat_base;
    [HideInInspector] public WeaponStat stat_upgrade; //stat yang akan ditambahkan

    public void InitializeStatBase()
    {
        fireRateText.text = "" + stat_base.fireRate;
        reloadTimeText.text = "" + stat_base.reloadTime;
        maxAmmoText.text = "" + stat_base.max_ammo;
        accuracyText.text = "" + stat_base.accuracy;
        damageText.text = "" + stat_base.damage;
        rangeText.text = "" + stat_base.range;
    }


    public void InitializeStatToUpgrade()
    {
        fireRate_upgradeText.text = "" + stat_base.fireRate;
        reloadTime_upgradeText.text = "" + stat_upgrade.reloadTime;
        maxAmmo_upgradeText.text = "" + stat_upgrade.max_ammo;
        accuracy_upgradeText.text = "" + stat_upgrade.accuracy;
        damage_upgradeText.text = "" + stat_upgrade.damage;
        range_upgradeText.text = "" + stat_upgrade.range;
    }
}
