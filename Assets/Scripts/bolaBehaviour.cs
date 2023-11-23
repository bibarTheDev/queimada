using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaBehaviour : MonoBehaviour
{
    private Rigidbody rigidBody;

    private bool isHeld = false;
    private bool isLethal = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getIsHeld() { return isHeld; }

    public GameObject bePickedUp()
    {    
        if(isHeld){
            return null;
        }

        // rigidBody.velocity = Vector3.zero;
        // rigidBody.angularVelocity = Vector3.zero; 

        isHeld = true;
        isLethal = false;

        return gameObject;
    }
    
    public void beThrown(Vector3 arremesso)
    {
        rigidBody.AddForce(arremesso);
        Debug.Log("FORCA");

        isHeld = false;
        isLethal = true;
    }
}
