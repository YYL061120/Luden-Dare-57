using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public string currentSceneName;

    [Header("Current Resources")]
    public int currentIronCount = 0;
    public int currentConcreteCount = 0;
    public int currentPeopleCount = 0;

    [Header("Initial Resources")]
    public int initialIronCount = 0;
    public int initialConcreteCount = 0;
    public int initialPeopleCount = 0;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        currentIronCount = initialIronCount;
        currentConcreteCount = initialConcreteCount;
        currentPeopleCount = initialPeopleCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && TypeWriterTest.isTyping == false)
        {
            TypeWriterTest.tyt.AnotherDialogue();
        }
    }




}
