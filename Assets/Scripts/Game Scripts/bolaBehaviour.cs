using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaBehaviour : MonoBehaviour
{
    private Rigidbody rigidBody;

    private bool isHeld = false;
    private Equipes throwerTeam = Equipes.None;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision coll)
    {
        // com quem colidou
        switch(coll.gameObject.tag){
        case "Quadra":
            // reseta arremesso
            throwerTeam = Equipes.None;
            // sfx
            SFXManager.instance.playBallHitGround();
            break;

        case "Parede":
            SFXManager.instance.playBallHitWall();
            // sfx
            break;

        case "Character":
            // sfx (deve ocorrer em playerBehaviour para garantir que nao vai dar problema)
            // SFXManager.instance.playBallHitPlayer();
            break;            
        }
    }

    public bool isBeignHeld() { return isHeld; }
    public bool isLethal(Equipes targetTeam) { return throwerTeam != Equipes.None && throwerTeam != targetTeam; }
    public void resetLethality() { throwerTeam = Equipes.None; }

    public GameObject bePickedUp(Equipes playerTeam)
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
