using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    private Vector3 initialScale;
    private Vector3 initalpos;

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
        initialScale = transform.localScale;
        initalpos = transform.position;
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
                        if (Input.GetKeyDown(KeyCode.Mouse0)|| Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            CurrentConstructCD += buildInteraction.buildInt.amountofBoost;
                            transform.DOShakePosition(0.15f,0.25f,25).OnComplete(() => transform.position=initalpos);
                            transform.DOShakeScale(0.15f,0.3f,30).OnComplete(() => transform.localScale = initialScale);

                        }
                    }
                    break;
                case 1:
                    constructionState = (GameManager.gameManager.currentIronCount >= 75);
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (constructionState&&!constructing)
                        {
                            GameManager.gameManager.currentIronCount -= 75;
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
                            GameManager.gameManager.currentIronCount -= 50;
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
                            GameManager.gameManager.currentIronCount -= 30;
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
                interacting = false;
                healthBar.gameObject.SetActive(true);
                able.SetActive(false);
                notAble.SetActive(false);
                progressing.SetActive(true);
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
                    Instantiate(HumanRoom, initalpos, Quaternion.identity);
                    
                    Destroy(this.gameObject);
                    break;
                case 2:
                    Instantiate(IronRoom, initalpos, Quaternion.identity);
                    
                    Destroy(this.gameObject);
                    break;
                case 3:
                    Instantiate(ConcreteRoom, initalpos, Quaternion.identity);
                    
                    Destroy(this.gameObject);
                    break;

            }
        }
    }



}
