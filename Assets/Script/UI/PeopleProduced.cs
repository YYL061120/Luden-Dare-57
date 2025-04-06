using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleProduced : MonoBehaviour
{
    public float forwardForce = 6f;   // 朝摄像机方向的力
    public float upwardForce = 8f;    // 垂直向上的力

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 朝向摄像机的方向（单位向量）
        Vector3 toCamera = (Camera.main.transform.position - transform.position).normalized;

        // 去掉Y分量，避免往下飞（我们自己决定上抛的高度）
        toCamera.y = 0f;
        toCamera.Normalize();

        // 合成抛射方向
        Vector3 throwDir = toCamera * forwardForce + Vector3.up * upwardForce;

        // 加力抛射
        rb.AddForce(throwDir, ForceMode.Impulse);

        SFXManager.sfxManager.PlayFallingScream();
    }

    void Update()
    {
        // Y轴旋转
        transform.Rotate(Vector3.up * 180f * Time.deltaTime, Space.World);

        // 销毁条件（例如飞出屏幕或掉太低）
        if (/*transform.position.y < -10f*/IsOffScreen(transform.position))
        {
            Debug.Log("En");
            Destroy(gameObject);
        }
    }

    public bool IsOffScreen(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // 如果在摄像机后面（Z<0），就当做已经不在屏幕内
        if (screenPos.z < 0)
            return true;

        if (screenPos.x < 0 || screenPos.x > Screen.width ||
            screenPos.y < 0 || screenPos.y > Screen.height)
        {
            return true;
        }

        return false;
    }
}
