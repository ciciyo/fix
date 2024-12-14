using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace HeavenFalls
{
    public class HealingCircle : MonoBehaviour
    {
        public GameObject aura_prefab;

        
        private List<Player> players;
        private List<GameObject> particle_auras;
        private List<GameObject> particle_auras_unused;


        public float heal_value = 50;
        [SerializeField] private float timeLife = 5f;

        [SerializeField] private float cooldown = 0.2f;
        [SerializeField] private float current_time;


        /// <summary>
        /// menyimpan data player dan particle aura yang digunakan
        /// jika masa aktif healing sudah habis maka setiap aura harus dihapus
        /// </summary>
        private Dictionary<Player, GameObject> auras;

        private void Awake()
        {
            players = new List<Player>();
            auras = new Dictionary<Player, GameObject>();
            particle_auras = new List<GameObject>();
            particle_auras_unused = new List<GameObject>();
        }
        private void Start()
        {
            
            Activate();
            current_time = 0f;
        }


        private void Update()
        {
            //membuat bisa menambahkan health dalam rate tertentu
            current_time += Time.deltaTime;

            if(current_time > cooldown)
            {
                AddHealthToAll();
                current_time = 0f;
            }

        }

        public void Activate()
        {
            StartCoroutine(OnActivate());
        }

        private IEnumerator OnActivate()
        {

            yield return new WaitForSeconds(timeLife);
            Destroy(gameObject);
            if(aura_prefab != null) RemoveAllAura();

        }

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


        public void AddHealthToAll()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i]) players[i].AddHeal(heal_value);
            }
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
                    if(aura_prefab != null)
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

                    if(aura_prefab != null)
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

