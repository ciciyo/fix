using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{

    /// <summary>
    /// Digunakan bersama efek ability yang memiliki lingkaran. misalnya healing mirip ara dan meteora
    /// sebaiknya disimpan di object yang sama dengan ability
    /// </summary>
    public class AuraSpawner : MonoBehaviour
    {
        public GameObject aura_prefab;
        private List<Player> players = new List<Player>();
        private List<GameObject> particle_auras;
        private List<GameObject> particle_auras_unused;
        /// <summary>
        /// menyimpan data player dan particle aura yang digunakan
        /// jika masa aktif healing sudah habis maka setiap aura harus dihapus
        /// </summary>
        private Dictionary<Player, GameObject> auras;


        

        private void RemoveAllAura()
        {
            for (int i = 0; i < particle_auras.Count; i++)
            {
                Destroy(particle_auras[i]);
            }

            for (int i = 0; i < particle_auras_unused.Count; i++)
            {
                Destroy(particle_auras_unused[i]);
            }
        }

        private void Awake()
        {
            players = new List<Player>();
            auras = new Dictionary<Player, GameObject>();
            particle_auras = new List<GameObject>();
            particle_auras_unused = new List<GameObject>();
        }


        private void OnDestroy()
        {
            RemoveAllAura();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                if (!players.Contains(player))
                {
                    players.Add(player);

                    GameObject aura = null;
                    //spawn aura
                    if (aura_prefab != null)
                    {
                        if (particle_auras_unused.Count > 0)
                        {
                            //sistem pooling
                            aura = particle_auras_unused[0];
                            aura.transform.parent = player.transform;
                            aura.transform.localPosition = Vector3.zero;
                            particle_auras.Add(aura);
                            particle_auras_unused.Remove(particle_auras_unused[0]);
                            aura.SetActive(true);
                        }
                        else
                        {
                            aura = Instantiate(aura_prefab, player.transform.position, Quaternion.identity);
                            aura.transform.SetParent(player.transform);
                            aura.transform.localPosition = Vector3.zero;
                            particle_auras.Add(aura);
                        }
                        if (aura != null) auras.Add(player, aura);
                    }

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                if (players.Contains(player))
                {
                    players.Remove(player);

                    if (aura_prefab != null)
                    {
                        GameObject aura = auras[player];
                        particle_auras_unused.Add(aura);
                        aura.SetActive(false);
                        aura.transform.SetParent(transform);
                        auras.Remove(player);
                    }

                }
            }
        }
    }
}