using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuadraStates
{
    Countdown,
    Game,
    EndOfGame
}

public class quadraManager : MonoBehaviour
{
    [Header("Game Settings")]
    public static int pontuacaoMax = 3;
    private int scoreA, scoreB; // eu poderia fazer isso com um Dict, mas NAO VOU (eles sao mt feios no c# seloco)

    // static reference    
    public static quadraManager instance;
    private QuadraStates state = QuadraStates.Countdown;

    
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

        scoreA = 0;
        scoreB = 0;
    }

    void onQueimaFunction(Equipes queimado)
    {
        Debug.Log(queimado + " foi queimado!");

        switch(queimado){
        case Equipes.A:
            scoreB += 1;
            onPonto?.Invoke(Equipes.B);
            if(scoreA >= pontuacaoMax){
                onVitoria?.Invoke(Equipes.B);
            }
            break;

        case Equipes.B:
            scoreA += 1;
            onPonto?.Invoke(Equipes.A);
            if(scoreA >= pontuacaoMax){
                onVitoria?.Invoke(Equipes.A);
            }
            break;

        default:
            return;
        }
    }
}

