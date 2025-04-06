using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFaceMoveEat : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // 获取物体与摄像机之间的方向
        Vector3 direction = -mainCamera.transform.position - transform.position;

        // 将方向的 Y 和 Z 轴忽略，只旋转物体在 X 和 Z 平面上
        direction.y = 0; // 忽略 Y 轴旋转（确保物体不会倾斜）

        // 旋转物体，使其始终面向摄像机
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
}
