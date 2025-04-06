using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uiManager;

    public string currentSceneName;

    [Header("Startscreen")]
    public GameObject CreditPage;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    //public void SceneIdentifier()
    //{
    //    switch (currentSceneName)
    //    {
    //        case "Startscreen":

    //            break;
    //    }
    //}

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
}
