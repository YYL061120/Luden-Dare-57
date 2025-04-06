using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public GameManager gameManager;
    public static SFXManager sfxManager;

    public AudioSource audioSource;
    [Header("Audio clips")]
    public AudioClip pScreamingOne;
    public AudioClip pScreamingTwo;
    public AudioClip pScreamingThree;

    private void Awake()
    {
        sfxManager = this;
    }
    public void PlayFallingScream()
    {
        int i = Random.Range(0, 3);
        if(i == 0) audioSource.PlayOneShot(pScreamingOne);
        if(i == 1) audioSource.PlayOneShot(pScreamingTwo);
        if(i == 2) audioSource.PlayOneShot(pScreamingThree);
    }
}
