using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGenericBehaviour : MonoBehaviour

{
    [Header("Default Behaviour")]
    public bool defaultHidden = true;

    protected CanvasGroup canvasGroup;

    // Start is called before the first frame update
    protected void Start() 
    { 
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if(canvasGroup == null){
            Debug.LogWarning(gameObject.name + ": nao foi encontrado canvasGroup component nesse objeto");
        }

        if(defaultHidden){
            hide();
        }
    }

    public void hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    public void show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
}
