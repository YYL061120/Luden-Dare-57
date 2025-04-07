using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changescreen : MonoBehaviour
{
    // Start is called before the first frame update
public void changeScenee()
    {
        SceneManager.LoadScene("entering");
    }
public void changeSceneg()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
