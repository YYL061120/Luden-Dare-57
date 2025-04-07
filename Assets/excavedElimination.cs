using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class excavedElimination : MonoBehaviour
{
    public GameObject[] children;
    public GameObject Able;
    public GameObject NotAble;
    public GameObject Underconstruction;

    void Start()
    {
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
        
    }

    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "StoneFace")
        {
            Destroy(this.gameObject);
        }


    }


}
