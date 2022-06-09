using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBubble : MonoBehaviour
{

    public ParticleSystem explosion;

    private void Start()
    {
        explosion.Pause();
    }

    public float explosionForce;
    public float explosionRadius;
    public float uplift;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position,explosionRadius,uplift);
            explosion.Play();
            Invoke(nameof(SelfDestroy), 0.3f);
        }
        else
        {
            SelfDestroy();
        }
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
    
}
