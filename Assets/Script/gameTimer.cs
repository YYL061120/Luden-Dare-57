using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class gameTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText; 
    public static float Thetimer;
    private float localTime;
    private bool isGameRunning = false;


    void Update()
    {        
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            isGameRunning = true;
            localTime += Time.deltaTime;
        }
        DisplayTIme();
        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            Thetimer = 0f;
        }
    }

    public void DisplayTIme()
    {
        isGameRunning = false;
        int minutes = Mathf.FloorToInt(localTime / 60f);
        int seconds = Mathf.FloorToInt(localTime % 60f);
        timeText.text = $"Time: {minutes:D2}:{seconds:D2}";
        Thetimer = localTime;
    }


}
