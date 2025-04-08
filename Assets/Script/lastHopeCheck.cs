using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastHopeCheck : MonoBehaviour
{
    public static lastHopeCheck LastHopeCheck;
    List<GameObject> LastHopeList = new List<GameObject>();
    public Boolean isthereHope;
    void Update()
    {
        if (LastHopeList.Count>0)
        {
            isthereHope = true;
            for (int i = 0; i < LastHopeList.Count; i++)
            {
                if (LastHopeList[i] == null)
                {
                    LastHopeList.RemoveAt(i);
                }
            }
        }
        if (LastHopeList.Count <= 0)
        {
            isthereHope = false;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag== "LastHope")
        {
            LastHopeList.Add(other.gameObject);
        }
    }
}
