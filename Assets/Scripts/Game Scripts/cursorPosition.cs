using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorPosition : MonoBehaviour
{
    [Header("General References")]
    public Camera mainCamera = null;

    [Header("KBM References")]
    public LayerMask groundRefLayer = 0;

    [Header("Pad References")]
    public GameObject playerObject = null;
    public float radius = 3.5f;

    private Transform characterTransform = null;
    private Vector3 previousDirection = Vector3.zero;
    private float cameraRotation;

    private ControllerType control = ControllerType.MKB;

    private bool enabledInputs = false;

    // event listening

    void subToEvents()
    {
        quadraManager.onGameStart += onGameStartFunction; 
        quadraManager.onGameEnd += onGameEndFunction; 
    }
    void unsubToEvents()
    {
        quadraManager.onGameStart -= onGameStartFunction;
        quadraManager.onGameEnd -= onGameEndFunction;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(mainCamera == null) {
            Debug.LogWarning(gameObject.name + ": Objeto camera nao foi configurado, impossivel executar");
        }
        else{
            // calcula a rotacao da camera em relacao a cena
            Vector3 cameraDirection = mainCamera.GetComponent<Transform>().forward;
            cameraDirection.y = 0;
            cameraDirection.Normalize();

            cameraRotation = Vector3.Angle(cameraDirection, new Vector3(0, 0, 1));
        }

        if(playerObject == null) {
            Debug.LogWarning(gameObject.name + ": Objeto player nao foi configurado, impossivel executar no modo Controle");
        }
        else{
            characterTransform = playerObject.GetComponent<Transform>();
        }
        
        subToEvents();
    }

    void Destroy() { unsubToEvents(); }

    public void setControllerType(ControllerType ct) { control = ct; }

    // Update is called once per frame
    void Update()
    {
        if(!enabledInputs){
            return;
        }

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

        direction = Quaternion.Euler(new Vector3(0, -cameraRotation, 0)) * direction.normalized;
        
        if(direction == Vector3.zero){
            direction = previousDirection;
        }
        else{
            previousDirection = direction;
        }

        // calcula a posicao em relacao ao player
        Vector3 result = characterTransform.position + (direction * radius);
        result.y = 0;
        return result;
    }

    void onGameStartFunction() { enabledInputs = true; }
    void onGameEndFunction(Equipes team) { enabledInputs = false; }
    
}
