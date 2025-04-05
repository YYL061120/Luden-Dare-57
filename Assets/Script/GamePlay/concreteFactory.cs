using System.Collections;
using UnityEngine;

public class concreteFactory : MonoBehaviour
{
    public GameManager manager;
    public bool canManufacture = false;

    [Header("Occupacy and Health")]
    public int maxOccupacy;
    public int currentPeople;
    public int maxHealth;
    public int currentHealth;

    [Header("Efficiency")]
    public int currentEfficiency = 5;

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
        Debug.Log("Concrete: "+manager.resource.concreteCount);
    }

    public IEnumerator Manufacturing()
    {
        int efficiency = 20;
        while (true)
        {
            if (currentPeople > 0) canManufacture = true;
            else canManufacture = false;
            if (canManufacture)
            {
                int addValue = currentEfficiency + currentPeople; //5材料/20s（+1材料/20s）
                manager.resource.ironCount += addValue;
            }
            yield return new WaitForSeconds(efficiency);
        }
    }

    public void HealthDeduction()
    {
        if (true)
        {

        }
    }
}
