using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    
    [SerializeField]
    PlayerScript playerScript;
    
    
    internal bool isLeftPressed;
    internal bool isRightPressed;
    internal bool isUpPressed;
    internal bool isSpaceDown;
    internal bool isSpaceUp;
    internal bool isSpacePressed;
    internal bool isDownPressed;
    internal bool isDashPressed;
    internal bool isThrowPressed;

    
    internal int xInput = 0;
    internal int yInput = 0;
    
    
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

        isUpPressed = Input.GetKey(KeyCode.UpArrow);

        isDashPressed = Input.GetKey(KeyCode.E);

        isThrowPressed = Input.GetKey(KeyCode.A);
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


        if (isUpPressed)
        {
            yInput = 1;
        }
        else
        {
            yInput = 0;
        }
    }
}
