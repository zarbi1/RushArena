using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject RedFlag;
    [SerializeField] private GameObject GreenFlag;
    [SerializeField] private Transform UpLimit;
    [SerializeField] private Transform LowLimit;
    [SerializeField] private float AnimTime = 5;
    [SerializeField] private ParticleSystem particleSystem;
    
    private MeshRenderer greenRenderer;
    private MeshRenderer redRenderer;

    private bool activated;
    
    private void Start()
    {
        particleSystem.Pause();
        activated = false;
        greenRenderer = GreenFlag.GetComponent<MeshRenderer>();
        redRenderer = RedFlag.GetComponent<MeshRenderer>();
        greenRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.gameObject.layer == 3)
        {
            //Debug.Log("Collided");
            other.gameObject.SendMessage("SetNewCheckPoint",transform.position);
            if (!activated)
            {
                activated = true;
                StartCoroutine(MoveFlag(RedFlag.transform, LowLimit.position, AnimTime));
                greenRenderer.enabled = true;
                StartCoroutine(MoveFlag(GreenFlag.transform, UpLimit.position, AnimTime));
            }
        }
    }

    public IEnumerator MoveFlag(Transform targ, Vector3 pos, float dur)
    {
        float t = 0f;
        Vector3 start = targ.position;
        Vector3 v = pos - start;
        while(t < dur)
        {
            t += Time.deltaTime;
            targ.position = start + v * t / dur;
            yield return null;
        }

        redRenderer.enabled = false;
        targ.position = pos;
        particleSystem.Play();
    }


}
