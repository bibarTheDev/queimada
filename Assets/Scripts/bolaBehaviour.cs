using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaBehaviour : MonoBehaviour
{
    private bool isHeld = false;
    private bool isLethal = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject bePickedUp()
    {    
        if(isHeld){
            return null;
        }

        isHeld = true;
        isLethal = false;

        return gameObject;
    }
    
    public void beThrown()
    {
        isHeld = false;
        isLethal = true;
    }
}
