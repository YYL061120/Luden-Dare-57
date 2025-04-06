using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private bool isCounting = false;
    [Header("Time")]
    public float playingDuration = 0;

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

        StartCounting();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsCounting();
    }

    public void StartCounting()
    {
        playingDuration = 0;
        isCounting = true;
    }
    
    public void IsCounting()
    {
        if (isCounting)
        {
            playingDuration += Time.deltaTime;
        }
    }
    
    public float EndCounting()
    {
        isCounting = false;
        return playingDuration;
    }




}
