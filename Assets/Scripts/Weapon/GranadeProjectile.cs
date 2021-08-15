using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GranadeProjectile : MonoBehaviour
{
    [SerializeField] private  float delay = 3f;
    [SerializeField] private float granadeRadius;
    [SerializeField] private float granadeSpeed;
    [SerializeField] private ParticleSystem _explosionParticleSystem;
    
    private float _countDown;
    private bool _hasExplosed;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * granadeSpeed, ForceMode.Impulse);
        _countDown = delay;
        
        var psShape = _explosionParticleSystem.shape;
        psShape.radius = granadeRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, granadeRadius);
    }

    void Update()
    {
        _countDown -= Time.deltaTime;
        if (_countDown <= 0f && !_hasExplosed)
        {
            Explode();
            _hasExplosed = true;
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
        ParticleSystem ps = Instantiate(_explosionParticleSystem, transform.position, Quaternion.identity);
        ps.Play();
        Destroy(gameObject);
    }
}