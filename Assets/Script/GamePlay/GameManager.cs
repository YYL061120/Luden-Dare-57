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

    //[System.Serializable]
    //public class Resource
    //{
    //    public int ironCount;
    //    public int concreteCount;
    //    public int peopleCount;

    //    public Resource(int ironCount, int concreteCount, int peopleCount)
    //    {
    //        this.ironCount = ironCount;
    //        this.concreteCount = concreteCount;
    //        this.peopleCount = peopleCount;
    //    }
    //}
    ////public Resource resource;
    //[Header("Resource")]
    //public int initialIronCount;
    //public int initialConcreteCount;
    //public int initialPeopleCount;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        //resource = new Resource(initialIronCount, initialConcreteCount, initialPeopleCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
