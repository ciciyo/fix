using HeavenFalls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMeteora : Ability
{
    public CharacterImprover prefab;
    public Transform spawn_point;

    protected override IEnumerator OnActivate()
    {
        var logAbility= $"{name}, \nAbility: {ability}";
        LogAbility.OnEventLog(logAbility);
        
        //spawn 
        CharacterImprover improver = Instantiate(prefab, spawn_point.position, Quaternion.identity);
        improver.transform.SetParent(spawn_point);
        controlView.CoolDown(cooldown_time);
        co_activate = null;
        yield break;
    }

    


}
