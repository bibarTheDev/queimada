using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOverlayBehaviour : MonoBehaviour

{
    [Header("Default Behaviour")]
    public bool defaultHidden = true;

    [Header("Overlay Settings")]
    public float alpha = 0.8f;

    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start() 
    { 
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        if(defaultHidden){
            hide();
        }
    }

    public void hide()
    {
        canvasGroup.alpha = 0;
    }

    public void show()
    {
        canvasGroup.alpha = alpha;
    }
}
