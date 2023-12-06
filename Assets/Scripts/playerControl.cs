using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : playerBehaviour
{
    [Header("Player Cursor Reference")]
    // objeto que representa a direcao do cursor (NAO o plano)
    public GameObject cursorPos = null;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        if(cursorPos == null) {
            Debug.LogWarning("Objeto cursorPos nao foi configurado, impossivel executar");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move(direction);

        if(Input.GetKey(KeyCode.Space)){
            pickBola();
        }
        if(Input.GetMouseButtonDown(0)){
            // pega a direcao do player ate o cursor
            // direcao = referencia - alvo
            Vector3 throwDirection = cursorPos.transform.position - this.transform.position;
            throwBola(throwDirection);
        }
    } 
}
