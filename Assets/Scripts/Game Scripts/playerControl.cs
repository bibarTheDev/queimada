using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : characterBehaviour
{
    [Header("Player Cursor Reference")]
    // objeto que representa a direcao do cursor (NAO o plano)
    public GameObject cursorPos = null;

    [Header("Input Settings")]
    public ControllerType controlType = 0;

    private bool inputsEnabled = false;

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
    new void Start()
    {
        base.Start();

        if(cursorPos == null) {
            Debug.LogWarning("Objeto cursorPos nao foi configurado, impossivel executar");
        }
        else{
            cursorPos.GetComponent<cursorPosition>().setControllerType(controlType);
        }

        subToEvents();        
    }
    
    void Destroy() { unsubToEvents(); }

    // Update is called once per frame
    new void Update()
    {
        if(!inputsEnabled){
            return;
        }

        // definitivamente nao eh a solucao mais elegante mas eh o que o prazo permite
        switch(controlType)
        {
        case ControllerType.Pad:
            doPadInputs();
            break;

        case ControllerType.MKB:
            doMKBInputs();
            break;
        }
    }

    void onGameStartFunction() { inputsEnabled = true; }
    void onGameEndFunction(Equipes team) { inputsEnabled = false; }

    void doPadInputs()
    {
        Vector3 direction = new Vector3(Input.GetAxis("HorizontalPad"), 0, Input.GetAxis("VerticalPad"));
        direction.z *= -1; // ?????
        move(direction);

        if(Input.GetButtonDown("PickUpPad")){
            pickBola();
        }
        if(Input.GetAxis("ThrowPad") > 0){
            // pega a direcao do player ate o cursor
            // direcao = referencia - alvo
            Vector3 throwDirection = cursorPos.transform.position - this.transform.position;
            throwBola(throwDirection);
        }

    }

    void doMKBInputs()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move(direction);

        if(Input.GetButtonDown("PickUp")){
            pickBola();
        }
        if(Input.GetButtonDown("Throw")){
            // pega a direcao do player ate o cursor
            // direcao = referencia - alvo
            Vector3 throwDirection = cursorPos.transform.position - this.transform.position;
            throwBola(throwDirection);
        }
    }
}
