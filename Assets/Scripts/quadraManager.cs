using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum quadraStates
{
    Countdown,
    Game,
    EndOfGame
}

public class quadraManager : MonoBehaviour
{
    public static quadraManager instance;

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

