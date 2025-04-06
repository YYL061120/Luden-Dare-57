using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnexcavedFloorControl : MonoBehaviour
{
    public static UnexcavedFloorControl unexcavedFloorControl;

    public GameObject UnexcavedFloorModle;
    public GameObject excavedFloor;
    public float defaultHealth;
    public float currentHealth;

    [Header("VibrantValue")]
    public float amplitude = 0.08f;
    public float frequency = 40f;
    private Vector3 initialLocalPosition;

    public healthBar healthBar;
    private void Awake()
    {
        unexcavedFloorControl = this;
    }
    void Start()
    {
        currentHealth = defaultHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(defaultHealth);
            healthBar.gameObject.SetActive(currentHealth!=defaultHealth);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(excavedFloor,this.gameObject.transform.position,Quaternion.identity);
            UnexcavedFloorGenerator.unexcavedFloorGenerator.theList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(currentHealth != defaultHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    public void Vibrating()
    {
        float offsetX = Mathf.Sin(Time.time * frequency) * amplitude;
        float offsetY = Mathf.Cos(Time.time * frequency) * amplitude;
        Vector3 vibrationOffset = new Vector3(offsetX, offsetY, 0f);
        UnexcavedFloorModle.transform.localPosition = initialLocalPosition + vibrationOffset;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "TheDrill")
        {
            Vibrating();
        }
    }


}
