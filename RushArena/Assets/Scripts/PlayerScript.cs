using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//=================================================
// The main player script
//=================================================
public class PlayerScript : MonoBehaviour
{
    //Store a reference to all the sub player scripts
    [SerializeField]
    internal PlayerInputScript inputScript;

    [SerializeField]
    internal PlayerMovementScript movementScript;

    [SerializeField]
    internal PlayerCollisionScript collisionScript;
    
    
    // Start is called before the first frame update
    void Start()
    {
        print("Main PlayerScript Starting");
    }

}
