using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorPosition : MonoBehaviour
{
    [Header("Mouse references (MKB)")]
    public Camera mainCamera = null;
    public LayerMask groundRefLayer = 0;

    [Header("Player reference (Pad)")]
    public GameObject playerObject = null;
    public float radius = 3.5f;

    private Transform characterTransform = null;
    private Vector3 previousDirection = Vector3.zero;

    private ControllerType control = ControllerType.MKB;

    // Start is called before the first frame update
    void Start()
    {
        if(mainCamera == null) {
            Debug.LogWarning(gameObject.name + ": Objeto camera nao foi configurado, impossivel executar no modo Mouse");
        }

        if(playerObject == null) {
            Debug.LogWarning(gameObject.name + ": Objeto player nao foi configurado, impossivel executar no modo Controle");
        }
        else{
            characterTransform = playerObject.GetComponent<Transform>();
        }
    }

    public void setControllerType(ControllerType ct) { control = ct; }

    // Update is called once per frame
    void Update()
    {
        switch(control)
        {
        case ControllerType.MKB:
            transform.position = positionAsMouse();
            break;
        case ControllerType.Pad:
            transform.position = positionAsController();
            break;
        }
    }

    Vector3 positionAsMouse()
    {
        // casta um raio da camera ate o proximo objeto
        Ray raio = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(raio, out RaycastHit hit, float.MaxValue, groundRefLayer)){
            // se posiciona onde o raio acertou
            return hit.point;
        }

        return transform.position;
    }

    Vector3 positionAsController()
    {
        // pega a direco do analogico
        Vector3 direction = new Vector3(Input.GetAxis("HorizontalAimPad"), 0, Input.GetAxis("VerticalAimPad"));
        direction.z *= -1; // ?????
        
        // Debug.Log("RStrick: " + direction);
        if(direction == Vector3.zero){
            direction = previousDirection;
        }
        else{
            previousDirection = direction;
        }

        // calcula a posicao em relacao ao player
        return characterTransform.position + (direction.normalized * radius);
    }
}
