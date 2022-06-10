using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerActionScript : MonoBehaviour
{
        [SerializeField] 
        PlayerScript PS;
        [SerializeField]
        internal GameObject TeleportAxe;

        [SerializeField] 
        internal float AxeThrowAngle = 0.5f;

        internal GameObject CurrentAxe;
        private bool AxeExists;
        private Rigidbody AxeRB;

        public ParticleSystem AxePS;

        private float axeTimer;
        
        private void Start()
        {
            AxeExists = false;
            axeTimer = 0;
            AxePS.Pause();
        }

        public void UpdateActions()
        {
            if (PS.inputScript.isThrowPressed)
            {
                if (!AxeExists  && axeTimer <= 0)
                {
                    
                    axeTimer = PS.axeCoolDown;
                    
                    CurrentAxe = Instantiate(TeleportAxe,PS.RB.position, Quaternion.identity);
                    int throwDirection = PS.movementScript.facingRight ? 1 : -1;
                    CurrentAxe.SendMessage("Init", PS.gameObject);
                    CurrentAxe.SendMessage("InitRotation", throwDirection);
                    
                    AxeExists = true;
                    AxeRB = CurrentAxe.GetComponent<Rigidbody>();
                    if (throwDirection == -1)
                    {
                        AxeRB.transform.Rotate(0,180,0);
                    }
                    AxeRB.AddForce(Vector2.Lerp(Vector2.right * throwDirection, Vector2.up, AxeThrowAngle) * PS.throwAxeForce * (1+PS.RB.velocity.x/85 * throwDirection), ForceMode.Impulse);
                }
                else
                {
                    CurrentAxe.SendMessage("OnCollisionEnter");
                }
                
            }

            bool up = axeTimer > 0;
            axeTimer -= Time.deltaTime;

            if (axeTimer <= 0 && up)
            {
                AxePS.Clear();
                AxePS.time = 0;
                AxePS.Play();
                Invoke(nameof(PauseAxePS),0.9f);
            }
        }

        
        public void SwitchPositions(Rigidbody AxeRB)
        {
            PS.RB.position = AxeRB.position;
            AxeExists = false;
            Destroy(CurrentAxe);
        }

        void PauseAxePS()
        {
            AxePS.Pause();
            AxePS.Clear();
        }

        
}
