using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInstrucoesBehaviour : UIGenericBehaviour
{
    // delegates
    public delegate void OnVoltarInstrClick();
    public static event OnVoltarInstrClick onVoltarInstrClick;
    
    public void voltarClick()
    {
        Debug.Log("Voltar");
        SFXManager.instance.playButtonClick();
        onVoltarInstrClick?.Invoke();
    }
}
