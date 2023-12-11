using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum QuadraStates
{
    Menu,
    Game,
    EndOfGame
}

public class quadraManager : MonoBehaviour
{
    [Header("Game Settings")]
    public static int pontuacaoMax = 3;
    private Dictionary<Equipes, int> scores; 

    // static reference    
    public static quadraManager instance;
    private QuadraStates state = QuadraStates.Menu;

    
    // delegates
    public delegate void OnEnterMenu();
    public static event OnEnterMenu onEnterMenu;
    public delegate void OnGameStart();
    public static event OnGameStart onGameStart;
    public delegate void OnPonto(Equipes ponto);
    public static event OnPonto onPonto;
    public delegate void OnGameEnd(Equipes vencedor);
    public static event OnGameEnd onGameEnd;

    // listeners
    void Awake()
    {
        characterBehaviour.onQueima += onQueimaFunction;
        UITitleBehaviour.onJogarClick += onJogarClickFunction;
    }
    void Destroy()
    {
        characterBehaviour.onQueima -= onQueimaFunction;
        UITitleBehaviour.onJogarClick -= onJogarClickFunction;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scores = new Dictionary<Equipes, int>();
        scores.Add(Equipes.A, 0);
        scores.Add(Equipes.B, 0);
        
        Debug.Log("SHOULD LOAD MENUS");
        onEnterMenu?.Invoke();
    }

    void onQueimaFunction(Equipes queimado)
    {
        if(state != QuadraStates.Game){
            return;
        }

        Equipes ponto = (queimado == Equipes.A) ? Equipes.B : Equipes.A;

        scores[ponto] += 1;
        onPonto?.Invoke(ponto);

        Debug.Log(scores[ponto] + ", " + pontuacaoMax);

        if(scores[ponto] >= pontuacaoMax){
            onGameEnd?.Invoke(ponto);
        }
    }

    void onJogarClickFunction()
    {
        if(state != QuadraStates.Menu){
            return;
        }

        state = QuadraStates.Game;
        onGameStart?.Invoke();
    }
}