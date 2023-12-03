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

public class playerControl : MonoBehaviour
{
    [Header("Player Cursor Reference")]
    public GameObject cursorPos = null;

    [Header("Player Stats")]
    public float moveSpeed = 1.0f;
    public float throwStrength = 1.0f;
    public float pickupRange = 1.0f;

    private BolaHeld bolaHeld = null;

    private Transform heldBolaPostion = null;

    // Start is called before the first frame update
    void Start()
    {
        if(cursorPos == null) {
            Debug.LogWarning("Objeto cursorPos nao foi configurado, impossivel executar");
        }

        // posicao do objeto bolaPosition
        heldBolaPostion = gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDelta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))/*.normalized*/;
        transform.Translate(moveDelta * moveSpeed * Time.deltaTime);
        if(bolaHeld != null){
            // move a bola ate o objeto bolaPosition
            bolaHeld.transform.position = heldBolaPostion.position;
        }

        if(Input.GetKey(KeyCode.Space)){
            pickBola();
        }
        if(Input.GetKey(KeyCode.LeftControl)){
            throwBola();
        }
    } 

    void pickBola()
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

    GameObject checkBolaNear()
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

    void throwBola()
    {
        if(bolaHeld == null){
            return;
        }

        // pega a direcao do player ate o cursor
        // direcao = referencia - alvo
        Vector3 throwDirection = cursorPos.transform.position - this.transform.position;
        Debug.Log(throwDirection);
        throwDirection.y = 0;
        Debug.Log(throwDirection);
        throwDirection = throwDirection.normalized * throwStrength;
        Debug.Log(throwDirection);
        

        bolaHeld.controller.beThrown(throwDirection);
        
        bolaHeld = null;
    }
}
