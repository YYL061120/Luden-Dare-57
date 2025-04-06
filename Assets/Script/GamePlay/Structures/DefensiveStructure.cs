using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveStructure : MonoBehaviour 
{
    public float maxHealth;
    public float currentHealth;

    private StoneFaceMoveEat ST;
    // Start is called before the first frame update
    void Start()
    {
        ST = GameObject.Find("StoneFace").GetComponent<StoneFaceMoveEat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            ST.facilitiesList.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void HealthDeduction()
    {

    }
}
