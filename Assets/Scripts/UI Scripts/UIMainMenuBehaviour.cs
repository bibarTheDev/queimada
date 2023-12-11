using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuBehaviour : UIGenericBehaviour
{
    [Header("Childs References")]
    public GameObject titleUIObject;
    public GameObject instrucoesUIObject;
    
    private UITitleBehaviour titleUIBehaviour;
    private UIInstrucoesBehaviour instrucoesUIBehaviour;

    new void Start()
    {
        base.Start();

        titleUIBehaviour = titleUIObject.GetComponent<UITitleBehaviour>();
        instrucoesUIBehaviour = instrucoesUIObject.GetComponent<UIInstrucoesBehaviour>();
    }

    public void showTitle()
    {
        base.show();
        titleUIBehaviour.show();
        instrucoesUIBehaviour.hide();
    }

    public void showInstructions()
    {
        base.show();
        instrucoesUIBehaviour.show();
        titleUIBehaviour.hide();
    }
}
