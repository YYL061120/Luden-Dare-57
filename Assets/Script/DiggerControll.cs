using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerControll : MonoBehaviour
{
    public int efficency;
    public Rigidbody DrillRB;
    public GameObject DigModel;

    [Header("VibrantValue")]
    public float amplitude = 0.1f;
    public float frequency = 15f;
    private Vector3 initialLocalPosition;


    private void Awake()
    {
        DrillRB = this.gameObject.GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        diggingDown();
        
    }



    public void diggingDown()
    {
        if (DrillRB != null)
        {
            DrillRB.AddForce(Vector3.down*efficency,ForceMode.Force);
        }
    }

    public void Vibrating()
    {
        float offsetX = Mathf.Sin(Time.time * frequency) * amplitude;
        float offsetY = Mathf.Cos(Time.time * frequency) * amplitude;
        Vector3 vibrationOffset = new Vector3(offsetX, offsetY, 0f);
        DigModel.transform.localPosition = initialLocalPosition + vibrationOffset;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "UnexcavedFloor")
        {
            Vibrating();
            collision.gameObject.GetComponent<UnexcavedFloorControl>().currentHealth -= efficency;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "UnexcavedFloor")
        {
            initialLocalPosition = DigModel.transform.localPosition;
        }
    }


}
