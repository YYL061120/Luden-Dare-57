using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class excavedElimination : MonoBehaviour
{
    public healthBar healthBar;
    public GameObject BarBG;
    public GameObject Able;
    public GameObject NotAble;
    public GameObject Underconstruction;

    public Boolean constructionState;
    public Boolean FloorAccesbility;
    public Boolean interacting;
    public Boolean constructing;

    public List<GameObject> childrenWithTag = new List<GameObject>();

    public string targetTag = "Room";
    private Vector3 initalpos;
    private Vector3 initialScale;

    public GameObject Defender;
    public float DefualtConstructCD;
    public float CurrentConstructCD;
    void Start()
    {
        FloorAccesbility = true;
        initalpos = this.transform.position;
        initialScale = this.transform.localScale;

        BarBG.SetActive(false);
        healthBar.gameObject.SetActive(false);
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.CompareTag(targetTag))
            {
                childrenWithTag.Add(child);
            }
        }
    }

    private void Update()
    {
        THefunction();
    }

    void FixedUpdate()
    {
        ConstructConcrete();
        indicatorUpdate();
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "StoneFace")
        {
            Destroy(this.gameObject);
        }


        if (other.gameObject.tag == "Incarnation")
        {
            for (int i = 0; i < childrenWithTag.Count; i++)
            {
                if (childrenWithTag[i] != null)
                {
                    if (childrenWithTag[i].GetComponent<Building>().constructing)
                    {
                        FloorAccesbility = false;
                    }
                }

            }
            interacting = true;
            switch (buildInteraction.buildInt.interactionMode)
            {
                case 0:
                    
                    break;

                case 4:
                    constructionState = (GameManager.gameManager.currentIronCount >= 100 && GameManager.gameManager.currentConcreteCount >= 100);
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (constructionState&& FloorAccesbility)
                        {
                            constructing = true;
                            GameManager.gameManager.currentIronCount -= 100;
                            GameManager.gameManager.currentConcreteCount -= 100;
                            healthBar.gameObject.SetActive(true);
                            BarBG.SetActive(true);
                            for (int i = 0; i < childrenWithTag.Count; i++)
                            {
                                Destroy(childrenWithTag[i]);
                            }
                        }
                    }
                    break;
            }
        }
    }

    public void THefunction() 
    {
        if(interacting)
        {
            if (constructing)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (buildInteraction.buildInt.interactionMode == 0)
                    {
                        CurrentConstructCD += buildInteraction.buildInt.amountofBoost;
                        transform.DOShakePosition(0.15f, 0.15f, 40).OnComplete(() => transform.position = initalpos);
                        transform.DOShakeScale(0.15f, 0.3f, 30).OnComplete(() => transform.localScale = initialScale);
                    }

                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Incarnation")
        {
            interacting = false;
        }
    }

        public void ConstructConcrete()
    {
        if (constructing)
        {
            healthBar.SetMaxHealth(DefualtConstructCD);
            healthBar.SetHealth(CurrentConstructCD);
            CurrentConstructCD += Time.deltaTime;
        }
        if(CurrentConstructCD>= DefualtConstructCD)
        {
            transform.position = initalpos;
            Instantiate(Defender, new Vector3(0.763f, transform.position.y, 0.49f), Quaternion.identity);
            Destroy(this.gameObject);
        }
        

    }


    public void indicatorUpdate()
    {
        if (!FloorAccesbility)
        {
            constructionState = false;
        }
        if (interacting)
        {
            if(buildInteraction.buildInt.interactionMode==4)
            {
                if (!constructing)
                {
                    Able.SetActive(constructionState);
                    NotAble.SetActive(!constructionState);
                }
                else
                {
                    interacting = false;
                    
                    Able.SetActive(false);
                    NotAble.SetActive(false);
                    Underconstruction.SetActive(true);
                }
            }
            else
            {
                Able.SetActive(false);
                NotAble.SetActive(false);
            }
        }

        else
        {
            Able.SetActive(false);
            NotAble.SetActive(false);
        }
    }

}
