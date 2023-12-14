using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private AudioSource audioSource;

    [Header("Audio Files")]
    public AudioClip buttonClick;
    public AudioClip startGame;
    public AudioClip ballHitPlayer;
    public AudioClip ballHitWall;
    public AudioClip ballHitGround;
    public AudioClip endGame;

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

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void playAudioClip(AudioClip clip) { audioSource.PlayOneShot(clip, 1.0f); }
    public void playButtonClick() { audioSource.PlayOneShot(buttonClick, 1.0f); }
    public void playStartGame() { audioSource.PlayOneShot(startGame, 0.5f); }
    public void playBallHitPlayer() { audioSource.PlayOneShot(ballHitPlayer, 1.0f); }
    public void playBallHitWall() { audioSource.PlayOneShot(ballHitWall, 1.0f); }
    public void playBallHitGround() { audioSource.PlayOneShot(ballHitGround, 1.0f); }
    public void playEndGame() { audioSource.PlayOneShot(endGame, 1.0f); }
}
