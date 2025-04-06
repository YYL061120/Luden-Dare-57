using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject able;
    public GameObject notAble;

    public Boolean constructionState;
    public Boolean interacting;
    void Start()
    {
        //GameManager.gameManager.currentIronCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        statusUpdate();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag== "Incarnation")
        {
            interacting = true;
            switch (buildInteraction.buildInt.interactionMode)
            {
                case 0:
                    
                    break;
                case 1:
                    constructionState = (GameManager.gameManager.currentPeopleCount >= 75);
                    
                    break;
                case 2:
                    constructionState = (GameManager.gameManager.currentIronCount >= 50);

                    break;
                case 3:
                    constructionState = (GameManager.gameManager.currentConcreteCount >= 30);

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
            able.SetActive(constructionState);
            notAble.SetActive(!constructionState);
        }
        else
        {
            able.SetActive(false);
            notAble.SetActive(false);
        }
            
        
    }
}
