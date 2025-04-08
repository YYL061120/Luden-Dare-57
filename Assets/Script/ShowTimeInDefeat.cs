using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowTimeInDefeat : MonoBehaviour
{
    public TextMeshProUGUI Display;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Defeat")
        {
            int minutes = Mathf.FloorToInt(gameTimer.Thetimer / 60f);
            int seconds = Mathf.FloorToInt(gameTimer.Thetimer % 60f);
            Display.text = $"Time: {minutes:D2}:{seconds:D2}";

        }
    }

}
