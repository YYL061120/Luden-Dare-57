using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class buildInteraction : MonoBehaviour
{
    public static buildInteraction buildInt;
    //public GameObject CurrentInteractingBlock;


    public float amountofBoost;
    public int interactionMode=0;
    //0=boosting
    //1=People
    //2=iron
    //3=concrete
    //4=upgrade Drill


    public GameObject HumanRoom;
    public GameObject IronRoom;
    public GameObject ConcreteRoom;

    public Sprite HumanRSp;
    public Sprite IronRSp;
    public Sprite ConcreteRSp;
    public Sprite BoostingSp;

    public SpriteRenderer StatesRenderer;
    void Start()
    {
        StatesRenderer.sprite = IronRSp;
        buildInt = this;
    }

    private void Update()
    {
        switchingActions();
    }

    private void FixedUpdate()
    {
        //ConscturctnewRoom();
        
    }

    public void switchingActions()
    {
        string input = Input.inputString;
        if (!string.IsNullOrEmpty(input))
        {
            char c = input[0];
            if (char.IsDigit(c))
            {
                int value = c - '0'; 
                if (value >= 0 && value <= 4)
                {
                    interactionMode = value;
                    actionUpdate(value);
                }
            }
        }
    }

    public void actionUpdate(int V)
    {
        switch (V)
        {
            case 0:
                StatesRenderer.sprite = BoostingSp;
                break;
            case 1:
                StatesRenderer.sprite = HumanRSp;
                break;
            case 2:
                StatesRenderer.sprite = IronRSp;
                break;
            case 3:
                StatesRenderer.sprite = ConcreteRSp;
                break;
        }
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Room")
        {
            CurrentInteractingBlock = collision.gameObject;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Room")
        {
            CurrentInteractingBlock = null;
        }
    }

    
    public void ConscturctnewRoom()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (CurrentInteractingBlock != null)
            {
                if (CurrentInteractingBlock.GetComponent<Building>().constructionState)
                {
                    switch (interactionMode)
                    {
                        case 1:
                            Instantiate(HumanRoom, CurrentInteractingBlock.transform.position, Quaternion.identity);
                            GameManager.gameManager.currentIronCount -= 75;
                            Destroy(CurrentInteractingBlock);
                            break;
                        case 2:
                            Instantiate(IronRoom, CurrentInteractingBlock.transform.position, Quaternion.identity);
                            GameManager.gameManager.currentIronCount -= 50;
                            Destroy(CurrentInteractingBlock);
                            break;
                        case 3:
                            Instantiate(ConcreteRoom, CurrentInteractingBlock.transform.position, Quaternion.identity);
                            GameManager.gameManager.currentIronCount -= 30;
                            Destroy(CurrentInteractingBlock);
                            break;
                    }
                    
                    
                }
            }
        }

        
    }*/
}
