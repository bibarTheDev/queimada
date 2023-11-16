using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    [Header("Player Stats")]
    public float moveSpeed = 1.0f;
    public float throwStrength = 1.0f;
    public float pickupRange = 1.0f;

    private GameObject bolaHeld = null;
    private bolaBehaviour bolaHeldController = null;
    private Transform bolaHeldTransform = null;

    private Transform heldBolaPostion = null;

    // Start is called before the first frame update
    void Start()
    {
        // posicao do objeto bolaPosition
        heldBolaPostion = gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDelta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(moveDelta * moveSpeed * Time.deltaTime);
        if(bolaHeldTransform != null){
            // TODO: resolver esses getComponents
            // move a bola ate o objeto bolaPosition
            bolaHeldTransform.position = heldBolaPostion.position;
        }

        if(Input.GetKey(KeyCode.Space)){
            if(bolaHeld == null){
                pickBola();
            }
            else {
                throwBola();
            }
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
            bolaHeldTransform = bola.GetComponent<Transform>();
            bolaHeldController = bola.GetComponent<bolaBehaviour>();
            bolaHeld = bolaHeldController.bePickedUp();
        }
    }

    GameObject checkBolaNear()
    {
        GameObject[] bolas = GameObject.FindGameObjectsWithTag("Bola");
        Debug.Log(bolas.Length);
        foreach (GameObject b in bolas){
            // se esta perto o suficiente E nao esta sendo segurado
            if (Vector3.Distance(transform.position, b.transform.position) < pickupRange
                && !b.GetComponent<bolaBehaviour>().getIsHeld()){
                    return b;
            }
        }

        return null;
    }

    void throwBola()
    {
        if(bolaHeld == null){
            return;
        }

        // TODO: resolver esses getComponents
        bolaHeldController.beThrown();
        
        bolaHeld = null;
        bolaHeldController = null;
        bolaHeldTransform = null;
    }
}
