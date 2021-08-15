using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    [SerializeField] private float maxLiveTime;
    [SerializeField] private float bulletSpeed = 1500f;
    
    private float _aliveTimer;


    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }

    private void Update()
    {
        _aliveTimer += Time.deltaTime;

        if (_aliveTimer >= maxLiveTime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var targetHit = collision.gameObject.GetComponent<Damageable>();

        if (targetHit != null)
        {
            targetHit.CheckHit(Damage, gameObject);
        }
        
        Destroy(gameObject);
    }
}