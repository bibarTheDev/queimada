using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuadraStates
{
    Menu,
    Countdown,
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
    private QuadraStates state = QuadraStates.Game;

    
    // delegates
    public delegate void OnPonto(Equipes ponto);
    public static event OnPonto onPonto;
    public delegate void OnVitoria(Equipes vencedor);
    public static event OnVitoria onVitoria;

    // listeners
    void Awake() { characterBehaviour.onQueima += onQueimaFunction; }
    void Destroy() { characterBehaviour.onQueima -= onQueimaFunction; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scores = new Dictionary<Equipes, int>();
        scores.Add(Equipes.A, 0);
        scores.Add(Equipes.B, 0);
    }

    void onQueimaFunction(Equipes queimado)
    {
        if(state != QuadraStates.Game){
            return;
        }

        Equipes ponto = (queimado == Equipes.A) ? Equipes.B : Equipes.A;

        scores[ponto] += 1;
        onPonto?.Invoke(ponto);
        if(scores[ponto] >= pontuacaoMax){
            onVitoria?.Invoke(ponto);
        }
    }
}

