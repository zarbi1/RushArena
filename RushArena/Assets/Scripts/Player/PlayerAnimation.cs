using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] PlayerScript PS;
        [SerializeField] internal GameObject playerAsset;
        [SerializeField] internal ParticleSystem FireballPickupEffect;
        [SerializeField] internal ParticleSystem CanDashEffect;

        private bool CanDashEffectPlaying;
        
        private Vector3 right = new Vector3(1, 1, -1);
        private Vector3 left = new Vector3(1, 1, 1);

        private Animation animator;

        private Renderer CanDashEffectRenderer;
        
        private GameObject root;

        private void Start()
        {
            root = GameObject.Find("Root");
            animator = root.GetComponent<Animation>();
            FireballPickupEffect.Pause();
            CanDashEffect.Play();
            CanDashEffectRenderer = CanDashEffect.GetComponent<Renderer>();
        }

        
        
        public void Update()
        {
            playerAsset.transform.localScale = PS.movementScript.facingRight ? left : right;

            if (CanDashEffectRenderer.enabled)
            {
                if (!(PS.movementScript.DashTimer <= 0))
                {
                    CanDashEffectRenderer.enabled = false; 
                }
            }
            else
            {
                if (PS.movementScript.DashTimer <= 0)
                {
                    CanDashEffectRenderer.enabled = true; 
                }
            }
            
            
            Debug.Log(CanDashEffect.isPlaying);
            
            
            if (Math.Abs(PS.RB.velocity.x) < 2)
            {
                animator.Play("idle");
            }
            else
            {
                animator.Play("run");
            }
            
        }

        void FireballPickup()
        {
            FireballPickupEffect.Clear();
            FireballPickupEffect.time = 0;
            FireballPickupEffect.Play();
            Invoke("PauseEffect",0.9f);
        }

        void PauseEffect()
        {
            FireballPickupEffect.Pause();
            FireballPickupEffect.Clear();
        }
    }
}