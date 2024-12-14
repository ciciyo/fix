using HeavenFalls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBomb : MonoBehaviour
{
    //public int damage;

    private Rigidbody rb;
    private bool targetHit;

    [Header("object to spawn on destroy")]
    public GameObject objectToAdd;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        //make sure only to stick to the first target you hit

        int groundLayer = LayerMask.NameToLayer("Ground");
        if (collision.gameObject.layer != groundLayer)
        {
            return;
        }

        if (targetHit)
            return;
        else targetHit = true;

        //check if you hit enemy
        /*if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }*/


        rb.isKinematic = true;
        transform.SetParent(collision.transform);

        
        if (objectToAdd)
        {
            GameObject obj = Instantiate(objectToAdd, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
