using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterTest : MonoBehaviour
{
    public GameObject stonehead;
    public GameObject digger;
    public GameObject conti;
    public GameObject text;
    public static TypeWriterTest tyt;
    public float typingSpeed = 0.05f;
    public string[] sentencesOne;
    public int currentSentenceIndex = 0;
    private TextMeshProUGUI textMeshPro;
    public static bool isTyping = false;
    public AudioClip typingSound;
    public AudioSource source;

    public int dialogueCount = 0;
    public int ck1;
    public int ck2;
    public int cke;
    public List<GameObject> dialogues = new List<GameObject>();

    private BoxCollider mouse;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        mouse = GameObject.Find("ThePlayerReincarnation").GetComponent<BoxCollider>();
        mouse.enabled = false;
        tyt = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            ShowNextSentence();
            if(currentSentenceIndex == ck1)
            {
                stonehead.SetActive(true);
            }
            if (currentSentenceIndex == ck2)
            {
                digger.SetActive(true);
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
        if (currentSentenceIndex == cke)
        {
            text.SetActive(false);
            conti.SetActive(true);
            mouse.enabled = true;
        }
        isTyping = false;
    }

    public void AnotherDialogue()
    {
        Debug.Log("index" + dialogueCount);
        dialogues[dialogueCount].SetActive(false);
        dialogueCount++;
        dialogues[dialogueCount].SetActive(true);
    }

    public void ActivateTypeWriter()
    {
        dialogues[dialogueCount].SetActive(true);
    }
}