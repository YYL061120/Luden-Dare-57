using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComicManager : MonoBehaviour
{
    public static ComicManager comicManager;
    public float typingSpeed = 0.05f;
    public string[] sentencesOne;
    public int currentSentenceIndex = 0;
    private TextMeshProUGUI textMeshPro;
    public bool isTyping = false;
    public AudioClip typingSound;
    public AudioSource source;

    [Header("2075")]
    public float fadeDuration = 3f;
    public GameObject two;

    [Header("Comics on TV")]
    public SpriteRenderer comicSprite;
    public List<Sprite> comics = new List<Sprite>();

    private Image fadeImage;
    public bool hasFadeIn = true;
    public float fadeInOutDuration = 3f;

    //Events
    public event Action onRequestMoveCamera;

    void Awake()
    {
        comicManager = this;
        fadeImage = GameObject.Find("FadeIn").GetComponent<Image>();
    }

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (hasFadeIn == true) StartCoroutine(FadeInOut(hasFadeIn));
        StartCoroutine(TwoDisappear());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping && two.activeSelf == false)
        {
            if (currentSentenceIndex >= sentencesOne.Length && hasFadeIn == false)
            {
                StartCoroutine(FadeInOut(hasFadeIn));
                hasFadeIn = true ;
            }
            else if(hasFadeIn == false) 
            {
                comicSprite.sprite = comics[currentSentenceIndex];
                ShowNextSentence();
            }
        }
    }

    public void ShowNextSentence()
    {
        if (currentSentenceIndex < sentencesOne.Length)
        {
            StartCoroutine(TypeSentence(sentencesOne[currentSentenceIndex]));
        }
        else
        {
            textMeshPro.text = "";
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        textMeshPro.text = "";
        if (typingSound != null && source)
        {
            source.PlayOneShot(typingSound);
        }
        foreach (char letter in sentence.ToCharArray())
        {
            textMeshPro.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        currentSentenceIndex++;

        if (currentSentenceIndex == 4)
        {
            onRequestMoveCamera?.Invoke();
        }
        else
        {
            isTyping = false;
        }
    }

    public void ResumeTypingAfterCameraMove()
    {
        isTyping = false;
        ShowNextSentence();
    }

    private IEnumerator TwoDisappear()
    {
        yield return new WaitForSeconds(fadeDuration);
        two.SetActive(false);
    }

    IEnumerator FadeInOut(bool hasFade)
    {
        if(hasFade == false)
        {
            float elapsedTime = 0f;
            Color initialColor = fadeImage.color;
            initialColor.a = 0f;

            fadeImage.color = initialColor;

            while (elapsedTime < fadeInOutDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                yield return null;
            }

            fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
            SceneManager.LoadScene("Gameplay");
        }
        else
        {
            float elapsedTime = 0f;
            Color initialColor = fadeImage.color;
            initialColor.a = 1;

            fadeImage.color = initialColor;

            yield return new WaitForSeconds(2f);

            while (elapsedTime < fadeInOutDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(1 - elapsedTime / fadeDuration);
                fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                yield return null;
            }

            fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
            hasFadeIn = false;
        }
    }
}
