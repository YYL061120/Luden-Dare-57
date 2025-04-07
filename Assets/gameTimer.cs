using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class gameTimer : MonoBehaviour
{
    public TMP_Text timeText; // ��ק UI Text �������
    private float timer = 0f;
    private bool isGameRunning = false;



        // ���� Gameplay �������ü�ʱ



    void Update()
    {        
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            timer = 0f;
            isGameRunning = true;           
            timer += Time.deltaTime;
        }            

    }

    // ����Ϸ����ʱ�����������
    public void OnGameEnd()
    {
        isGameRunning = false;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timeText.text = $"��ʱ��{minutes:D2}:{seconds:D2}";
    }
}
