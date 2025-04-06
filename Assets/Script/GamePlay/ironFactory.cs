using NUnit.Framework.Internal;
using System.Collections;
using UnityEngine;

public class ironFactory : MonoBehaviour
{
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
        StartCoroutine(Manufacturing());
    }
    private void Update()
    {
        //Debug.Log("Iron: " + manager.resource.ironCount);
    }

    public IEnumerator Manufacturing()
    {
        int efficiency = 10;
        while (true)
        {
            if (currentPeople > 0) canManufacture = true;
            else canManufacture = false;
            if (canManufacture)
            {
                int addValue = currentEfficiency + currentPeople; //formula: 5钢/10s（+1钢/10s）
                GameManager.gameManager.currentIronCount += addValue;
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
