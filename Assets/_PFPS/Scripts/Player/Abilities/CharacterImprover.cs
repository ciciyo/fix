using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace HeavenFalls
{
    public class CharacterImprover : MonoBehaviour
    {

        public float timeLife = 7f;
        private List<Player> players;
        [Range(0,1)] public float improve_rate = 1f;
        private bool enableImprove = true;


        private void Awake()
        {
            players = new List<Player>();
        }
        private void Start()
        {
            
            Activate();
        }


        public void Activate()
        {
            if (!enableImprove) return;

            StartCoroutine(OnActivate());
        }

        private IEnumerator OnActivate()
        {
            enableImprove = true;

            yield return new WaitForSeconds(timeLife);
            enableImprove = false;
            for (int i = 0; i < players.Count; i++)
            {
                float damage = players[i].weapon.config.gunProperties.damage/(1+improve_rate);
                players[i].weapon.config.gunProperties.damage =  (int)damage;
            }

            Destroy(gameObject);
        }



        private void OnTriggerEnter(Collider other)
        {
            if (!enableImprove) return;

            if(other.TryGetComponent(out Player player))
            {
                players.Add(player);

                if (player.weapon)
                {
                    float damage = player.weapon.config.gunProperties.damage * (1+improve_rate);
                    player.weapon.config.gunProperties.damage = (int)damage;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!enableImprove) return;

            if (other.TryGetComponent(out Player player))
            {
                if (players.Contains(player))
                {
                    players.Remove(player);
                    if (player.weapon)
                    {
                        float damage = player.weapon.config.gunProperties.damage;
                        player.weapon.config.gunProperties.damage = (int)(damage/(1+improve_rate));
                    }
                }
            }
        }

    }
}