using System.Collections;
using UnityEngine;

public class peopleFactory : MonoBehaviour
{
    public bool canManufacture;
    public GameObject peopleProduced;

    [Header("Occupacy and Health")]
    public int maxOccupacy;
    public int currentPeople;
    public float maxHealth;
    public float currentHealth;

    private void Awake()
    {

    }

    private void Start()
    {
        StartCoroutine(Manufacturing());
    }
    private void Update()
    {
        //Debug.Log("People: "+manager.resource.peopleCount);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
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
                GameManager.gameManager.currentPeopleCount++;
                PeoplePopOutEffect();   
            }
            float waitingTime = efficiency - currentPeople * 3f; //formula: 60s/人（-3s/人）
            yield return new WaitForSeconds(waitingTime);
        }

    }

    public void HealthDeduction()
    {
        if (true)
        {

        }
    }

    public void PeoplePopOutEffect()
    {
        Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        Vector3 spawnPos = transform.position + offset;
        Quaternion randomRot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

        Instantiate(peopleProduced, spawnPos, randomRot);
    }
}
