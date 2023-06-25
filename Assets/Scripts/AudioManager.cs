using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [Header("SFX")]
    [Header("Menu")]
    [SerializeField] AudioClip buttonClickAudioClip;
    [SerializeField] AudioClip worldMovementAudioClip;


    [Header("Gameplay")]
    [SerializeField] AudioClip wrongCombinationAudioClip;
    [SerializeField] AudioClip successCombinationAudioClip;
    [SerializeField] AudioClip levelCompleteAudioClip;
    [SerializeField] AudioClip gameOverAudioClip;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playButtonClickAudioClip()
    {
        audioSource.PlayOneShot(buttonClickAudioClip, 1f);
    }

    public void playWorldMovementAudioClip()
    {
        audioSource.PlayOneShot(worldMovementAudioClip, 1f);
    }

    public void playWrongCombinationAudioClip()
    {
        audioSource.PlayOneShot(wrongCombinationAudioClip, 1f);
    }

    public void playSuccessCombinationAudioClip()
    {
        audioSource.PlayOneShot(successCombinationAudioClip, 1f);
    }

    public void playLevelCompleteAudioClip()
    {
        audioSource.PlayOneShot(levelCompleteAudioClip, 1f);
    }

    public void playGameOverAudioClip()
    {
        audioSource.PlayOneShot(gameOverAudioClip, 1f);
    }

}
