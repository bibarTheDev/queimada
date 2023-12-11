using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinBehaviour : UIGenericBehaviour
{
    private GameObject P1Text, P2Text;

    // delegates
    public delegate void OnContinueWinClick();
    public static event OnContinueWinClick onContinueWinClick;

    // Start is called before the first frame update
    new void Start() 
    { 
        base.Start();
        
        P1Text = gameObject.transform.GetChild(0).gameObject;
        P2Text = gameObject.transform.GetChild(1).gameObject;
        Debug.Log("P1Text check on " + gameObject.name + ": " + P1Text.name);
        Debug.Log("P2Text check on " + gameObject.name + ": " + P2Text.name); 
    }

    public void show(Equipes winner)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;

        switch(winner)
        {
        case Equipes.A:
            P1Text.SetActive(true);
            P2Text.SetActive(false);
            break;
            
        case Equipes.B:
            P2Text.SetActive(true);
            P1Text.SetActive(false);
            break;   
        }
    }

    public void continueClick()
    {
        Debug.Log("Contiue");
        onContinueWinClick?.Invoke();
    }
}
