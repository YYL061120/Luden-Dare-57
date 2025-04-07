using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class gameTimer : MonoBehaviour
{
    public TMP_Text timeText; // 拖拽 UI Text 组件进来
    private float timer = 0f;
    private bool isGameRunning = false;



        // 仅在 Gameplay 场景启用计时



    void Update()
    {        
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            timer = 0f;
            isGameRunning = true;           
            timer += Time.deltaTime;
        }            

    }

    // 在游戏结束时调用这个方法
    public void OnGameEnd()
    {
        isGameRunning = false;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timeText.text = $"用时：{minutes:D2}:{seconds:D2}";
    }
}
