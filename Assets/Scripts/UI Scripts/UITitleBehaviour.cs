using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitleBehaviour : UIGenericBehaviour
{
    public void jogarClick()
    {
        Debug.Log("Jogar");
    }
    
    public void instrucoesClick()
    {
        Debug.Log("Instrucoes");
    }
    
    public void sairClick()
    {
        Debug.Log("Eh o quitas");
        Application.Quit();
    }
}
