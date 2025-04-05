using UnityEngine;

public class concreteFactory : MonoBehaviour
{
    public GameManager manager;

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
    }
    private void Update()
    {
        //Debug.Log("Concrete: "+manager.resource.concreteCount);
        Manufacturing();
    }

    public void Manufacturing()
    {
        if (currentPeople > 0)
        {
            
        }
    }

    public void HealthDeduction()
    {
        if (true)
        {

        }
    }
}
