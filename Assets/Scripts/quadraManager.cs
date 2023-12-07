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
    public int pontuacaoMax = 3;

    // static reference    
    public static quadraManager instance;
    private QuadraStates state = QuadraStates.Countdown;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

