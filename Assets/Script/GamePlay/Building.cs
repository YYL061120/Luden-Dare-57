using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    public healthBar healthBar;
    public GameObject able;
    public GameObject notAble;
    public GameObject progressing;

    public Boolean constructionState;
    public Boolean interacting;
    public Boolean constructing;

    public GameObject HumanRoom;
    public GameObject IronRoom;
    public GameObject ConcreteRoom;

    public float DefualtConstructCD;
    public float CurrentConstructCD;
    private int FutureBuildingType;
    void Start()
    {
        //GameManager.gameManager.currentIronCount;
        constructing = false;
        CurrentConstructCD = 0;
        FutureBuildingType = 0;
        healthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        statusUpdate();
        whileconstructing();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag== "Incarnation")
        {
            interacting = true;
            switch (buildInteraction.buildInt.interactionMode)
            {
                case 0:
                    if (constructing)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            CurrentConstructCD += buildInteraction.buildInt.amountofBoost;
                        }
                    }
                    break;
                case 1:
                    constructionState = (GameManager.gameManager.currentIronCount >= 75);
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (constructionState&&!constructing)
                        {
                            
                            FutureBuildingType = 1;
                            constructing = true;
                        }
                    }
                        break;
                case 2:
                    constructionState = (GameManager.gameManager.currentIronCount >= 50);
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (constructionState && !constructing)
                        {
                            FutureBuildingType = 2;
                            constructing = true;
                        }
                    }
                    break;
                case 3:
                    constructionState = (GameManager.gameManager.currentIronCount >= 30);
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (constructionState && !constructing)
                        {
                            FutureBuildingType = 3;
                            constructing = true;
                        }
                    }

                    break;
                case 4:
                    interacting = false;
                    break;

            }
                 
        }
        
    }

    

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Incarnation")
        {
            interacting = false;
        }
    }

    public void statusUpdate()
    {
        if (interacting)
        {
            if (!constructing)
            {
                able.SetActive(constructionState);
                notAble.SetActive(!constructionState);
            }
            else
            {
                healthBar.gameObject.SetActive(true);
                able.SetActive(!constructionState);
                notAble.SetActive(!constructionState);
                progressing.SetActive(constructionState);
            }
            
        }
        else
        {
            able.SetActive(false);
            notAble.SetActive(false);
        }
            
        
    }


    public void whileconstructing()
    {
        if (constructing)
        {
            healthBar.SetMaxHealth(DefualtConstructCD);
            healthBar.SetHealth(CurrentConstructCD);
            CurrentConstructCD += Time.deltaTime;

        }
        if (CurrentConstructCD >= DefualtConstructCD)
        {
            switch (FutureBuildingType)
            {
                case 1:
                    Instantiate(HumanRoom, this.transform.position, Quaternion.identity);
                    GameManager.gameManager.currentIronCount -= 75;
                    Destroy(this.gameObject);
                    break;
                case 2:
                    Instantiate(IronRoom, this.transform.position, Quaternion.identity);
                    GameManager.gameManager.currentIronCount -= 75;
                    Destroy(this.gameObject);
                    break;
                case 3:
                    Instantiate(ConcreteRoom, this.transform.position, Quaternion.identity);
                    GameManager.gameManager.currentIronCount -= 75;
                    Destroy(this.gameObject);
                    break;

            }
        }
    }



}
