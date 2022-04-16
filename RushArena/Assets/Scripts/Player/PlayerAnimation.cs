using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] PlayerScript PS;
        [SerializeField] internal GameObject playerAsset;

        private Vector3 right = new Vector3(1, 1, -1);
        private Vector3 left = new Vector3(1, 1, 1);

        private Animation animator;

        private GameObject root;
        
        private void Start()
        {
            root = GameObject.Find("Root");
            animator = root.GetComponent<Animation>();
        }

        public void Update()
        {
            playerAsset.transform.localScale = PS.movementScript.facingRight ? left : right;
            if (Math.Abs(PS.RB.velocity.x) < 2)
            {
                animator.Play("idle");
            }
            else
            {
                animator.Play("run");
            }
            
        }

        

    }
}