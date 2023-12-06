using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// agrupa componentes da bola
public class BolaHeld {
    public GameObject obj = null;
    public bolaBehaviour controller = null;
    public Transform transform = null;

    public BolaHeld(GameObject ob, bolaBehaviour ct, Transform tr)
    {
        this.obj = ob;
        this.controller = ct;
        this.transform = tr;
    }
}

public class playerBehaviour : MonoBehaviour
{
    [Header("Player Team")]
    public string playerTeam = "A";

    [Header("Player Stats")]
    public float moveSpeed = 1.0f;
    public float throwStrength = 1.0f;
    public float pickupRange = 1.0f;

    protected BolaHeld bolaHeld = null;

    protected Transform heldBolaPostion = null;
    protected CharacterController controller = null;

    // Start is called before the first frame update
    protected void Start()
    {
        // posicao do objeto bolaPosition
        heldBolaPostion = gameObject.transform.GetChild(1);

        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected void Update()
    {
        // should implement
    } 

    protected void OnCollisionEnter(Collision coll)
    {
        // note que esse eh o colisor presente no objeto Player (e nao o colisor em PlayerBody)
        GameObject obj = coll.gameObject;

        // se foi atingido por uma bola e se ela era lethal
        if(obj.tag == "Bola" && obj.GetComponent<bolaBehaviour>().getIsLethal()){
            Debug.Log("Ai");
        }
    }

    protected void move(Vector3 direction)
    {
        if(direction.magnitude > 1){
            direction.Normalize();
        }
        controller.Move(direction * moveSpeed * Time.deltaTime);
        // transform.Translate(direction * moveSpeed * Time.deltaTime);

        if(bolaHeld != null){
            // move a bola ate o objeto bolaPosition
            bolaHeld.transform.position = heldBolaPostion.position;
        }
    }

    protected void pickBola()
    {
        if(bolaHeld != null){
            return;
        }
        
        // pega a bola mais proxima (se existir)
        GameObject bola = checkBolaNear();

        if(bola != null){

            // segura a bola (se estiver disponivel)
            bolaHeld = new BolaHeld(
                bola,
                bola.GetComponent<bolaBehaviour>(),
                bola.GetComponent<Transform>()
            );

            bolaHeld.controller.bePickedUp();
        }
    }

    private GameObject checkBolaNear()
    {
        GameObject[] bolas = GameObject.FindGameObjectsWithTag("Bola");
        GameObject selectedBola = null;

        // pickup range age como uma distancia maxima
        float prevDistance = pickupRange;

        foreach (GameObject b in bolas){
            float distance = Vector3.Distance(transform.position, b.transform.position);
            // se esta mais perto que a ultima E nao esta sendo segurado
            if (distance < prevDistance && !b.GetComponent<bolaBehaviour>().getIsHeld()){
                prevDistance = distance;
                selectedBola = b;
            }
        }

        return selectedBola;
    }

    protected void throwBola(Vector3 throwDirection)
    {
        if(bolaHeld == null){
            return;
        }

        // remove o componente vertical (y) e normaliza
        throwDirection.y = 0;
        throwDirection = throwDirection.normalized * throwStrength;
        
        bolaHeld.controller.beThrown(throwDirection);
        
        bolaHeld = null;
    }
}
