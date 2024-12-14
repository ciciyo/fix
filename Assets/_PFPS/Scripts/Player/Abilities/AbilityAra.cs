using System.Collections;
using System.Collections.Generic;
using HeavenFalls;
using UnityEngine;

public class AbilityAra : Ability
{
    public HealingBomb healingBomb_prefab;
    public HealingBomb healingBomb;


    protected override IEnumerator OnActivate()
    {
        var logAbility= $"{name}, \nAbility: {ability}";
        LogAbility.OnEventLog(logAbility);
        
        healingBomb.Throw();
        controlView.CoolDown(cooldown_time);
        co_activate = null;
        yield break;
    }
}
