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

        internal GameObject CurrentAxe;
        private bool AxeExists;

        private void Start()
        {
            AxeExists = false;
        }

        public void UpdateActions()
        {
            if (PS.inputScript.isThrowPressed)
            {
                if (!AxeExists)
                {
                    CurrentAxe = Instantiate(TeleportAxe,PS.RB.position, Quaternion.identity);
                    CurrentAxe.SendMessage("Init", PS.gameObject);
                    AxeExists = true;
                }
                else
                {
                    CurrentAxe.SendMessage("OnCollisionEnter");
                }
                
            }
        }


        public void SwitchPositions(Rigidbody AxeRB)
        {
            (PS.RB.position, AxeRB.position) = (AxeRB.position, PS.RB.position);
            Destroy(CurrentAxe);
            AxeExists = false; 
        }
}
