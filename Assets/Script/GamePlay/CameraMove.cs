using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public float scrollSpeed = 5f; // 控制滚动移动的速度

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            Vector3 newPosition = transform.position + new Vector3(0f, scrollInput * scrollSpeed, 0f);
            transform.position = newPosition;
        }
    }
}
