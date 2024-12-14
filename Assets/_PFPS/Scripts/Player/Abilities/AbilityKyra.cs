using System.Collections;
using System.Collections.Generic;
using HeavenFalls;
using UnityEngine;

public class AbilityKyra : Ability
{
    public ParticleSystem vfxBurstFire;
    
    protected override IEnumerator OnActivate()
    {
        var logAbility= $"{name}, \nAbility: {ability}";
        LogAbility.OnEventLog(logAbility);
        
        vfxBurstFire.Play();
        controlView.CoolDown(cooldown_time);
        co_activate = null;
        yield break;
    }
}
