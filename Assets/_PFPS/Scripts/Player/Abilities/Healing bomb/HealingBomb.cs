using HeavenFalls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBomb : MonoBehaviour
{

    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public Transform objectToTHrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;


    


    private void Start()
    {
        readyToThrow = true;
    }

    public void Throw()
    {
        readyToThrow = false;

        //instantiate object to throw
        GameObject projectile = Instantiate(objectToTHrow.gameObject, attackPoint.position, cam.rotation);

        //add force
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        
        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        //add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;


        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        totalThrows--;

        //implement cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    
    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
