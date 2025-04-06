using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "Gameplay")
        {
            peopleText = people.GetComponent<TextMeshProUGUI>();
            concreteText = concrete.GetComponent<TextMeshProUGUI>();
            ironText = iron.GetComponent<TextMeshProUGUI>();
        }
    }

    private void LateUpdate()
    {
        UpdateDisplayingUI();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
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
        peopleText.text = GameManager.gameManager.currentIronCount.ToString();
        concreteText.text = GameManager.gameManager.currentConcreteCount.ToString();
        ironText.text = GameManager.gameManager.currentIronCount.ToString() ;
    }
}
