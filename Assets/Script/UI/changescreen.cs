using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changescreen : MonoBehaviour
{
    public static changescreen Changescreen;

    private void Start()
    {
        Changescreen = this;
    }
    public void changeScenee()
    {
        SceneManager.LoadScene("entering");
    }
public void changeSceneg()
    {
        SceneManager.LoadScene("Gameplay");
    }
public void changeScenes()
    {
        SceneManager.LoadScene("Startscreen");
    }
    public void changeSceneToDefeat()
    {
        SceneManager.LoadScene("Defeat");
    }
}
