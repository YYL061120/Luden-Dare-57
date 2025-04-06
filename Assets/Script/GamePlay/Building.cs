using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject able;
    public GameObject notAble;

    public int constructionState;

    void Start()
    {
        GameManager.gameManager.currentIronCount++;
        Debug.Log("gm pc: " + GameManager.gameManager.currentPeopleCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag== "Incarnation")
        {
            
        }
        
    }

    public void statusUpdate()
    {

    }
}
