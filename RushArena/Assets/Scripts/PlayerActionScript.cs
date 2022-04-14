using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerActionScript : MonoBehaviour
    {
        [SerializeField] 
        PlayerScript PS;

        private List<GameObject> Axes;

        void Start()
        {
            Axes = new List<GameObject>();
        }
        
        public void UpdateActions()
        {
            if (PS.inputScript.isThrowPressed)
            {
               // Axes.Add(Instantiate());   
            }
        }
        
    }
}