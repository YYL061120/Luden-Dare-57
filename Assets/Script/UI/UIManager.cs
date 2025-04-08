using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;
public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uiManager;

    [Header("Resources Display")]
    public GameObject people;
    public GameObject concrete;
    public GameObject iron;

    private TextMeshProUGUI peopleText;
    private TextMeshProUGUI concreteText;
    private TextMeshProUGUI ironText;

    public string currentSceneName;

    [Header("Startscreen")]
    public GameObject CreditPage;

    [Header("Tutorial Pages")]
    public GameObject Tutorial;
    public List<GameObject> tutorialPages = new List<GameObject>();
    public int tutorialPageIndex = 0;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "Defeat")
        {
            Tutorial = GameObject.Find("Tutorial");
            Tutorial.SetActive(false);
            if (currentSceneName == "Entering" || currentSceneName == "Gameplay")
            {
                peopleText = people.GetComponent<TextMeshProUGUI>();
                concreteText = concrete.GetComponent<TextMeshProUGUI>();
                ironText = iron.GetComponent<TextMeshProUGUI>();
            }
        }
        else
        {

        }
    }

    private void FixedUpdate()
    {
        if(currentSceneName != "Defeat")
        {
            tutorialPageIndex = Mathf.Clamp(tutorialPageIndex, 0, tutorialPages.Count - 1);
            for (int i = 0; i < tutorialPages.Count; i++)
            {
                if (i != tutorialPageIndex) tutorialPages[i].gameObject.SetActive(false);
            }
            tutorialPages[tutorialPageIndex].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        Alternative();
    }

    private void LateUpdate()
    {
        UpdateDisplayingUI();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Comic");
        uiManager = this;
    }

    public void Credit()
    {
        if (CreditPage.activeSelf == false)
            CreditPage.SetActive(true);
        else
            CreditPage.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateDisplayingUI()
    {
        if (peopleText != null)
        {
            peopleText.text = "Popolation :" + GameManager.gameManager.currentPeopleCount.ToString();

        }
        if (concreteText != null)
        {
            concreteText.text = "Concrete :"+GameManager.gameManager.currentConcreteCount.ToString();
        }
        if (ironText != null)
        {
            ironText.text = "Iron :" + GameManager.gameManager.currentIronCount.ToString();
        }

       
        
    }

    public void NextPage()
    {
        tutorialPageIndex++;
    }

    public void PreviousPage()
    {
        tutorialPageIndex--;
    }

    public void OCTutorial()
    {
        if(Tutorial.activeSelf == false)
        {
            Tutorial.SetActive(true);
        }
        else
        {
            Tutorial.SetActive(false) ;
        }
    }

    public void Alternative()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousPage();
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPage();
        }
    }
}
