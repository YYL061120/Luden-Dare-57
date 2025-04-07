using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public GameObject[] texts;
    public GameObject digger;
    public GameObject stonehead;
    public int index = 0;    
    public int checkp1;
    public int checkp2;

    void OnEnable()
    {
        index = 0;
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].SetActive(false);
        }
        texts[0].SetActive(true);
    }
    // Start is called before the first frame update
    // Update is called once per fram
    public void ClickButton()
    {
        if (index < texts.Length - 1)
        {
            texts[index].SetActive(false);
            index++;
            if (index == checkp1)
            {
                texts[index].SetActive(true);            
                if (stonehead.activeSelf == false)
            {
                stonehead.SetActive(true);
            }
            }
            if(index == checkp2)
            {
                texts[index].SetActive(false);
                if (digger.activeSelf==false)
                {
                    digger.SetActive(true);
                }
            }
            texts[index].SetActive(true);
            
        }
        
        else
        {

            gameObject.SetActive(false);
            texts[index].SetActive(false);
        }
    }
}
