using System.Collections;
using TMPro;
using UnityEngine;

public class TypeWriterTest : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    public string[] sentencesOne;
    public int currentSentenceIndex = 0;
    private TextMeshProUGUI textMeshPro;
    private bool isTyping = false;
    public AudioClip typingSound;
    public AudioSource source;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            ShowNextSentence();
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

        foreach (char letter in sentence.ToCharArray())
        {
            textMeshPro.text += letter;

            if (typingSound != null && source)
            {
                source.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        currentSentenceIndex++;
        isTyping = false;
    }
}