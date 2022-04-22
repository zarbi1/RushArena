using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] internal float bounceForce = 1;
    [SerializeField] internal float intensityDelay = 1;
    [SerializeField] internal float intensityBoost = 1;
    private Light light;
    private float normal_intensity;
    private void Start()
    {
        light = GetComponent<Light>();
        normal_intensity = light.intensity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == 3)
        {
            collision.rigidbody.velocity = new Vector3(collision.rigidbody.velocity.x,0,collision.rigidbody.velocity.z);
            collision.rigidbody.AddForce(Vector3.up * bounceForce,ForceMode.Impulse);
            light.intensity *= intensityBoost;
            Invoke("ReduceIntensity", intensityDelay);
        }
    }

    void ReduceIntensity()
    {
        light.intensity = normal_intensity;
    }
}
