using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public class AbilityZorek : Ability
    {
        public LaserGreen laserGreen;
        public float duration = 5f;

        

        


        


        protected override IEnumerator OnActivate()
        {
            laserGreen.duration = duration;
            laserGreen.Attack();
            
            yield return new WaitForSeconds(duration);
            controlView.CoolDown(cooldown_time);
            
            co_activate = null;
            yield break;
        }
    }
}