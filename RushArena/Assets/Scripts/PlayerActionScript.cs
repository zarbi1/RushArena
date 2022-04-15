using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerActionScript : MonoBehaviour
{
        [SerializeField] 
        PlayerScript PS;
        [SerializeField]
        internal GameObject TeleportAxe;
        
        public void UpdateActions()
        {
            if (PS.inputScript.isThrowPressed)
            {
                GameObject a = Instantiate(TeleportAxe,PS.RB.position, Quaternion.identity);
            }
        }
        
}
