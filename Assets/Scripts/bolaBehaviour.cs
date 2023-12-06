using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaBehaviour : MonoBehaviour
{
    private Rigidbody rigidBody;

    private bool isHeld = false;
    private string throwerTeam = "";

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public bool isBeignHeld() { return isHeld; }
    public bool isLethal(string playerTeam) { return throwerTeam != playerTeam; }

    public GameObject bePickedUp(string playerTeam)
    {    
        if(isHeld){
            return null;
        }

        // rigidBody.velocity = Vector3.zero;
        // rigidBody.angularVelocity = Vector3.zero; 

        isHeld = true;
        throwerTeam = playerTeam;

        return gameObject;
    }
    
    public void beThrown(Vector3 arremesso)
    {
        // reseta qualquer momento e dps adiciona o arremesso (era a gravidade!!)
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(arremesso);

        isHeld = false;
    }
}
