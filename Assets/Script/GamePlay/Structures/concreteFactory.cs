using System.Collections;
using UnityEngine;

public class concreteFactory : MonoBehaviour
{
    public bool canManufacture = false;

    [Header("Occupacy and Health")]
    public int maxOccupacy;
    public int currentPeople;
    public float maxHealth;
    public float currentHealth;

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
        //Debug.Log("Concrete: "+manager.currentConcreteCount);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
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
                GameManager.gameManager.currentConcreteCount += addValue;
            }
            yield return new WaitForSeconds(efficiency);
        }
    }

    public void HealthDeductingEffect()
    {
        
    }
}
