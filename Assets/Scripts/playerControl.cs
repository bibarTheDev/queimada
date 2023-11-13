using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    [Header("Player Stats")]
    public float moveSpeed = 1.0f;
    public float throwStrength = 1.0f;
    public float pickupRange = 1.0f;

    public GameObject bolaTeste = null;

    private GameObject bolaHold = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDelta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(moveDelta * moveSpeed * Time.deltaTime);
        if(bolaHold != null){
            // TODO: resolver esses getComponents
            bolaHold.GetComponent<Transform>().Translate(moveDelta * moveSpeed * Time.deltaTime);
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
        if(bolaHold != null){
            return;
        }
        
        // pega a bola mais proxima (se existir)
        GameObject bola = checkBolaNear();


        if(bola != null){
            // segura a bola (se estiver disponivel)
            // TODO: resolver esses getComponents
            bolaHold = bola.GetComponent<bolaBehaviour>().bePickedUp();

        }
    }

    GameObject checkBolaNear()
    {
        return bolaTeste;
    }

    void throwBola()
    {
        if(bolaHold == null){
            return;
        }

        // TODO: resolver esses getComponents
        bolaHold.GetComponent<bolaBehaviour>().beThrown();
        bolaHold = null;
    }
}
