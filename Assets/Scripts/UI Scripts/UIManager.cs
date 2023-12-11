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
    public GameObject mainMenuUIObject;
    public GameObject titleUIObject;
    
    private UIWinBehaviour winUIBehaviour;
    private UIGameBehaviour gameUIBehaviour;
    private UIOverlayBehaviour overlayUIBehaviour;
    private UIMainMenuBehaviour mainMenuUIBehaviour;
    private UITitleBehaviour titleUIBehaviour;

    // listeners
    void Awake()
    { 
        quadraManager.onEnterMenu += onEnterMenuFunction;
        quadraManager.onGameStart += onGameStartFunction;
        quadraManager.onPonto += onPontoFunction;
        quadraManager.onGameEnd += onGameEndFunction;
        UITitleBehaviour.onInstrucoesClick += onInstrucoesClickFunction;
        UIInstrucoesBehaviour.onVoltarInstrClick += onVoltarInstrClickFunction;
    }
    void Destroy()
    { 
        quadraManager.onEnterMenu -= onEnterMenuFunction;
        quadraManager.onGameStart -= onGameStartFunction;
        quadraManager.onPonto -= onPontoFunction;
        quadraManager.onGameEnd -= onGameEndFunction;
        UITitleBehaviour.onInstrucoesClick -= onInstrucoesClickFunction;
        UIInstrucoesBehaviour.onVoltarInstrClick -= onVoltarInstrClickFunction;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("winUIObject check on " + gameObject.name + ": " + winUIObject.name);
        // Debug.Log("gameUIObject check on " + gameObject.name + ": " + gameUIObject.name);
        // Debug.Log("overlayUIObject check on " + gameObject.name + ": " + overlayUIObject.name);
        // Debug.Log("mainMenuUIObject check on " + gameObject.name + ": " + mainMenuUIObject.name);
        // Debug.Log("titleUIObject check on " + gameObject.name + ": " + titleUIObject.name);

        winUIBehaviour = winUIObject.GetComponent<UIWinBehaviour>();
        gameUIBehaviour = gameUIObject.GetComponent<UIGameBehaviour>();
        overlayUIBehaviour = overlayUIObject.GetComponent<UIOverlayBehaviour>();
        mainMenuUIBehaviour = mainMenuUIObject.GetComponent<UIMainMenuBehaviour>();
        titleUIBehaviour = titleUIObject.GetComponent<UITitleBehaviour>();
    }


    public void onEnterMenuFunction()
    {
        mainMenuUIBehaviour.showTitle();
        overlayUIBehaviour.show();

        gameUIBehaviour.hide();
        winUIBehaviour.hide();
    }

    public void onGameStartFunction()
    {
        gameUIBehaviour.show();

        winUIBehaviour.hide();
        overlayUIBehaviour.hide();
        mainMenuUIBehaviour.hide();
    }

    public void onPontoFunction(Equipes team) { gameUIObject.GetComponent<UIGameBehaviour>().marcarPonto(team); }

    public void onGameEndFunction(Equipes team)
    {
        winUIBehaviour.show(team);
        overlayUIBehaviour.show();

        gameUIBehaviour.hide();
        mainMenuUIBehaviour.hide();
    }
    
    public void onInstrucoesClickFunction() { mainMenuUIBehaviour.showInstructions(); }

    public void onVoltarInstrClickFunction() { mainMenuUIBehaviour.showTitle(); }

}
