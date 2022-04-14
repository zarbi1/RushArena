using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] PlayerScript PS;
        [SerializeField] internal GameObject playerAsset;

        private Vector3 right = new Vector3(1, 1, -1);
        private Vector3 left = new Vector3(1, 1, 1);

        public void Update()
        {
            playerAsset.transform.localScale = PS.movementScript.facingRight ? left : right;
            
        }

        

    }
}