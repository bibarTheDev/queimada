using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Childs References")]
    public GameObject winUIObject;
    public GameObject gameUIObject;

    // listeners
    void Awake()
    { 
        quadraManager.onGameStart += onGameStartFunction;
        quadraManager.onPonto += onPontoFunction;
        quadraManager.onGameEnd += onGameEndFunction;
    }
    void Destroy()
    { 
        quadraManager.onGameStart -= onGameStartFunction;
        quadraManager.onPonto -= onPontoFunction;
        quadraManager.onGameEnd -= onGameEndFunction;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("winUIObject check on " + gameObject.name + ": " + winUIObject.name);
        Debug.Log("gameUIObject check on " + gameObject.name + ": " + gameUIObject.name);
    }

    public void onGameStartFunction()
    {
        gameUIObject.GetComponent<UIGameBehaviour>().show();
        winUIObject.GetComponent<UIWinBehaviour>().hide();
    }

    public void onPontoFunction(Equipes team)
    {
        gameUIObject.GetComponent<UIGameBehaviour>().marcarPonto(team);
    }

    public void onGameEndFunction(Equipes team)
    {
        gameUIObject.GetComponent<UIGameBehaviour>().hide();
        winUIObject.GetComponent<UIWinBehaviour>().show(team);
    }
}
