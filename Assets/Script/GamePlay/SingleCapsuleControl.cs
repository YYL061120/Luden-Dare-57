using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCapsuleControl : MonoBehaviour
{
    private float currentT;
    private SpriteRenderer therenderer;
    public Sprite F1;
    public Sprite F2;
    public Sprite F3;
    public Sprite F4;
    public Sprite F5;
    public float DisplayLast;
    [SerializeField] private float currentFrame;
    void Start()
    {
        therenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        animationPlay();
    }


    public void animationPlay()
    {
        currentT += Time.deltaTime;
        if(currentT>= DisplayLast)
        {
            if (currentFrame > 5)
            {
                currentFrame = 1;
            }
            currentFrame++;
            currentT = 0;
            
        }
        switch (currentFrame)
        {
            case 1:
                therenderer.sprite = F1;
                break;
            case 2:
                therenderer.sprite = F2;
                break;
            case 3:
                therenderer.sprite = F3;
                break;
            case 4:
                therenderer.sprite = F4;
                break;
            case 5:
                therenderer.sprite = F5;
                break;

        }
    }
}
