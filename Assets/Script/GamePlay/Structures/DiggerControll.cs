using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerControll : MonoBehaviour
{
    public int efficency;
    public Rigidbody DrillRB;
    public GameObject DigModel;
    public GameObject DrillGIF;
    public List<GameObject> ListOfGif = new List<GameObject>();

    [Header("VibrantValue")]
    public float amplitude = 0.1f;
    public float frequency = 15f;
    private Vector3 initialLocalPosition;

    public int AmountOfResources;
    public int AmountToIncrease;
    public Boolean Selected;
    public Boolean Constructable;
    public GameObject Able;
    public GameObject NotAble;
    private void Awake()
    {
        DrillRB = this.gameObject.GetComponent<Rigidbody>();
    }
    void Start()
    {

    }
    private void Update()
    {
        PlayerChoosing();
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        diggingDown();
        DrillGitAdd(efficency);
        StatusUpdate();
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

    public void DrillGitAdd(int numofD)
    {
        if(ListOfGif.Count< numofD)
        {
            for(int i=0;i< numofD - ListOfGif.Count; i++)
            {
                float xrange = UnityEngine.Random.RandomRange(-0.25f, 1.5f);
                float Yrange = UnityEngine.Random.RandomRange(-0.2f, -0.5f);
                GameObject Temp = Instantiate(DrillGIF, new Vector3(xrange, DigModel.transform.position.y+Yrange, 0), Quaternion.identity, DigModel.transform);
                ListOfGif.Add(Temp);
            }
            
        }
        
    }

    public void PlayerChoosing()
    {
        if (buildInteraction.buildInt.interactionMode == 5)
        {
            Selected = true;
            Constructable = (GameManager.gameManager.currentIronCount >= AmountOfResources && GameManager.gameManager.currentConcreteCount >= AmountOfResources && GameManager.gameManager.currentPeopleCount >= AmountOfResources);
            if (Input.GetMouseButtonDown(0))
            {
                if (Constructable)
                {
                    GameManager.gameManager.currentIronCount -= AmountOfResources;
                    GameManager.gameManager.currentConcreteCount -= AmountOfResources;
                    GameManager.gameManager.currentPeopleCount -= AmountOfResources;
                    AmountOfResources += AmountToIncrease;
                    efficency++;
                }
            }
        }
        else
        {
            Selected = false;
        }
    }




    public void StatusUpdate()
    {
        if (Selected)
        {
            
                Able.SetActive(Constructable);
                NotAble.SetActive(!Constructable);
            
        }
        else
        {
            Able.SetActive(false);
            NotAble.SetActive(false);
        }
    }
}
