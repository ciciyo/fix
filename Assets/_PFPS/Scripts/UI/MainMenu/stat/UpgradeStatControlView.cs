using HeavenFalls;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public interface IStatControlView
{
    public void InitializeStatBase();
    public void InitializeStatToUpgrade();
}

public class UpgradeStatControlView : MonoBehaviour, IStatControlView
{
    [Header("stat base")]
    public Slider slider_hp;
    public Slider slider_damage;
    public Slider slider_shield;
    public Slider slider_defense;
    public Slider slider_speed;
    public Slider slider_skill;

    [Header("stat upgrade preview")]
    public TMP_Text hp_upgradeText;
    public TMP_Text stamina_upgradeText;
    public TMP_Text damage_upgradeText;
    public TMP_Text defense_upgradeText;
    public TMP_Text speed_upgradeText;
    public TMP_Text skill_upgradeText;

    [HideInInspector] public ConfigStats stat_base;
    [HideInInspector] public ConfigStats stat_upgrade; //stat yang akan ditambahkan

    public void InitializeStatBase()
    {
        float max_value = SO_CharacterData.MAX_STATEVALUE;

        if(slider_hp) slider_hp.value = stat_base.health/max_value;
        if(slider_skill) slider_skill.value = stat_base.skills / max_value;
        if(slider_speed) slider_speed.value = stat_base.speed / max_value;
        if(slider_damage) slider_damage.value = stat_base.damage / max_value;
        if(slider_defense) slider_defense.value = stat_base.defense / max_value;
        if(slider_shield) slider_shield.value = stat_base.stamina / max_value;
    }


    public void InitializeStatToUpgrade()
    {
        /*hp_upgradeText.text = "" + stat_upgrade.health;
        skill_upgradeText.text = "" + stat_upgrade.skills;
        speed_upgradeText.text = "" + stat_upgrade.speed;
        damage_upgradeText.text = "" + stat_upgrade.damage;
        defense_upgradeText.text = "" + stat_upgrade.defense;
        stamina_upgradeText.text = "" + stat_upgrade.stamina;*/
    }

}
