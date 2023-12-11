using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Childs References")]
    public GameObject winUIObject;
    public GameObject gameUIObject;
    public GameObject overlayUIObject;
    
    private UIWinBehaviour winUIBehaviour;
    private UIGameBehaviour gameUIBehaviour;
    private UIOverlayBehaviour overlayUIBehaviour;

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
        Debug.Log("overlayUIObject check on " + gameObject.name + ": " + overlayUIObject.name);

        winUIBehaviour = winUIObject.GetComponent<UIWinBehaviour>();
        gameUIBehaviour = gameUIObject.GetComponent<UIGameBehaviour>();
        overlayUIBehaviour = overlayUIObject.GetComponent<UIOverlayBehaviour>();
    }

    public void onGameStartFunction()
    {
        gameUIBehaviour.show();
        winUIBehaviour.hide();
        overlayUIBehaviour.hide();
    }

    public void onPontoFunction(Equipes team)
    {
        gameUIObject.GetComponent<UIGameBehaviour>().marcarPonto(team);
    }

    public void onGameEndFunction(Equipes team)
    {
        gameUIBehaviour.hide();
        winUIBehaviour.show(team);
        overlayUIBehaviour.show();
    }
}
