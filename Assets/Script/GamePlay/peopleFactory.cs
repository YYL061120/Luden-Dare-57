using System.Collections;
using UnityEngine;

public class peopleFactory : MonoBehaviour
{
    public GameManager manager;
    public bool canManufacture;

    [Header("Occupacy and Health")]
    public int maxOccupacy;
    public int currentPeople;
    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {

    }

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Manufacturing());
    }
    private void Update()
    {
        //Debug.Log("People: "+manager.resource.peopleCount);
    }

    public IEnumerator Manufacturing()
    {
        int efficiency = 60;
        while (true)
        {
            if (currentPeople > 0) canManufacture = true;
            else canManufacture = false;
            if (canManufacture)
            {
                manager.currentPeopleCount++;
            }
            float waitingTime = efficiency - manager.currentPeopleCount * 3f; //formula: 60s/人（-3s/人）
            yield return new WaitForSeconds(waitingTime);
        }

    }

    public void HealthDeduction()
    {
        if (true)
        {

        }
    }
}
