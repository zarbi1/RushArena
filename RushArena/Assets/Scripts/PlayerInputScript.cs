using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    
    [SerializeField]
    PlayerScript playerScript;
    
    
    internal bool isLeftPressed;
    internal bool isRightPressed;
    internal bool isSpaceDown;
    internal bool isSpaceUp;
    internal bool isSpacePressed;
    internal bool isDownPressed;
    internal int xInput = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerInput script starting");
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
         * Pour chaque touche on check si elle est pressée
         * si elle ne l'est pas on remet à false 
         */

        #region Récup des inputs
        
        isLeftPressed = Input.GetKey(KeyCode.LeftArrow);
        
        isRightPressed = Input.GetKey(KeyCode.RightArrow);
        
        isSpaceDown = Input.GetKeyDown(KeyCode.Space);
        
        isSpaceUp = Input.GetKeyUp(KeyCode.Space);

        isSpacePressed = Input.GetKey(KeyCode.Space);

        isDownPressed = Input.GetKey(KeyCode.DownArrow);
        
        #endregion
        
        if (isRightPressed == isLeftPressed)
        {
            xInput = 0;
        }
        else if (isRightPressed)
        {
            xInput = 1;
        }
        else 
        {
            xInput = -1;
        }
    }
}
