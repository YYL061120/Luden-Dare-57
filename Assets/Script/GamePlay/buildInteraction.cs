using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class buildInteraction : MonoBehaviour
{
    public static buildInteraction buildInt;

    public TextMeshProUGUI testword;

    public int interactionMode=0;
    //0=boosting
    //1=People
    //2=iron
    //3=concrete
    //4=upgrade Drill

    void Start()
    {
        buildInt = this;
    }


    void Update()
    {
        switchingActions();
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
                    testword.text = ""+value;
                }
            }
        }
    }
}
