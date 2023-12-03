using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorPosition : MonoBehaviour
{
    [Header("Mouse references")]
    public Camera camera = null;
    public LayerMask groundRefLayer = 0;

    [Header("Player reference")]
    public GameObject playerObject = null;

    private bool isMouse = true;


    // Start is called before the first frame update
    void Start()
    {
        if(camera == null) {
            Debug.LogWarning("Objeto camera nao foi configurado, impossivel executar no modo Mouse");
        }

        if(playerObject == null) {
            Debug.LogWarning("Objeto player nao foi configurado, impossivel executar no modo Controle");
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = (isMouse) ? positionAsMouse() : positionAsController();
    }

    Vector3 positionAsMouse()
    {
        // casta um raio da camera ate o proximo objeto
        Ray raio = camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(raio, out RaycastHit hit, float.MaxValue, groundRefLayer)){
            // se posiciona onde o raio acertou
            return hit.point;
        }

        return transform.position;
    }

    Vector3 positionAsController()
    {
        // :)
        return transform.position;
    }
}
