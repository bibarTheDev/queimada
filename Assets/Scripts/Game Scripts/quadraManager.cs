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
    void subToEvents()
    {
        characterBehaviour.onQueima += onQueimaFunction;
        UITitleBehaviour.onJogarClick += onJogarClickFunction;
        UITitleBehaviour.onSairClick += onSairClickFunction;
        UIWinBehaviour.onContinueWinClick += onContinueWinClickFunction;
    }
    void unsubToEvents()
    {
        characterBehaviour.onQueima -= onQueimaFunction;
        UITitleBehaviour.onJogarClick -= onJogarClickFunction;
        UITitleBehaviour.onSairClick -= onSairClickFunction;
        UIWinBehaviour.onContinueWinClick -= onContinueWinClickFunction;
    }

    // Start is called before the first frame update
    void Start()
    {
        // singleton setup
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Debug.LogWarning(gameObject + ": impedido de ser criado");
            Destroy(gameObject);
        }
        // esse metodo precisa de um delay para dar tempo de tudo carrregar
        Invoke(nameof(setupQuadra), 0.2f);
        subToEvents();
    }

    void Destroy() { unsubToEvents(); }

    void setupQuadra()
    {
        state = QuadraStates.Menu;

        scores = new Dictionary<Equipes, int>();
        scores.Add(Equipes.A, 0);
        scores.Add(Equipes.B, 0);
        
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

        Debug.Log(scores[ponto] + "/" + pontuacaoMax);

        if(scores[ponto] >= pontuacaoMax){
            // sfx
            SFXManager.instance.playEndGame();

            // state change e trigger
            state = QuadraStates.EndOfGame;
            onGameEnd?.Invoke(ponto);
        }
    }

    void onJogarClickFunction()
    {
        if(state != QuadraStates.Menu){
            return;
        }

        // sfx
        SFXManager.instance.playStartGame();

            // state change e trigger
        state = QuadraStates.Game;
        onGameStart?.Invoke();
    }

    void onSairClickFunction() { Application.Quit(); }

    void onContinueWinClickFunction()
    {
        if(state != QuadraStates.EndOfGame){
            return;
        }

        setupQuadra();
    }
}