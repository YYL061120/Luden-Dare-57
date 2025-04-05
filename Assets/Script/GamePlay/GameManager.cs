using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string currentSceneName;

    [System.Serializable]
    public class Resource
    {
        public int ironCount;
        public int concreteCount;
        public int peopleCount;

        public Resource(int ironCount, int concreteCount, int peopleCount)
        {
            this.ironCount = ironCount;
            this.concreteCount = concreteCount;
            this.peopleCount = peopleCount;
        }
    }
    public Resource resource;
    [Header("Resource")]
    public int initialIronCount;
    public int initialConcreteCount;
    public int initialPeopleCount;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        resource = new Resource(initialIronCount, initialConcreteCount, initialPeopleCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
