using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    
    [SerializeField]
    
    PlayerScript playerScript;
    
    
    internal bool isLeftPressed;
    internal bool isRightPressed;
    internal bool isSpacePressed;
    internal bool isDownPressed;
    
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
        
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            isLeftPressed = true;
        }
        else
        {
            isLeftPressed = false;
        }
        
        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            isRightPressed = true;
        }
        else
        {
            isRightPressed = false;
        }
        
        
        if(Input.GetKey(KeyCode.Space))
        {
            isSpacePressed = true;
        }
        else
        {
            isSpacePressed = false;
        }
        
        
        if(Input.GetKey(KeyCode.DownArrow))
        {
            isDownPressed = true;
        }
        else
        {
            isDownPressed = false;
        }
    }
}
