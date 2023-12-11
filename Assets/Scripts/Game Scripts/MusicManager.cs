using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;

    // listeners
    void subToEvents()
    { 
        quadraManager.onEnterMenu += onEnterMenuFunction;
        quadraManager.onGameStart += onGameStartFunction;
        quadraManager.onGameEnd += onGameEndFunction;
    }
    void unsubToEvents()
    { 
        quadraManager.onEnterMenu -= onEnterMenuFunction;
        quadraManager.onGameStart -= onGameStartFunction;
        quadraManager.onGameEnd -= onGameEndFunction;
    }

    // Start is called before the first frame update
    void Start()
    {
        musicSource = gameObject.GetComponent<AudioSource>();
        subToEvents();
    }

    void Destroy() { unsubToEvents(); }

    void setMusicLow()
    { 
        musicSource.volume = 0.5f;
        musicSource.pitch = 0.9f;
    }
    void setMusicHigh()
    {
        musicSource.volume = 1.0f;
        musicSource.pitch = 1.0f;
    }

    void onEnterMenuFunction() { setMusicLow(); }
    void onGameStartFunction() { setMusicHigh(); }
    void onGameEndFunction(Equipes e) { setMusicHigh(); }
}
