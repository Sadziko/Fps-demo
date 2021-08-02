using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GranadeProjectile : MonoBehaviour
{
    [SerializeField] private  float delay = 3f;
    [SerializeField] private  float granadeRadius = 5000f;
    [SerializeField] private float granadeSpeed;
    

    private float countDown;
    private bool hasExplosed;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * granadeSpeed, ForceMode.Impulse);
        countDown = delay;
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, granadeRadius);
    }

    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExplosed)
        {
            Explode();
            hasExplosed = true;
        }
    }

    private void Explode()
    {
        Collider[] touchedObjects = Physics.OverlapSphere(transform.position, granadeRadius)
            .ToArray();

        foreach (Collider touchedObject in touchedObjects)
        {
            var targetHit = touchedObject.gameObject.GetComponent<Damageable>();

            if (targetHit != null)
            {
                targetHit.CheckHit(1, gameObject);
            }
        }

        Destroy(gameObject);
    }
}