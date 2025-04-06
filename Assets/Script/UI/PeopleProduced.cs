using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleProduced : MonoBehaviour
{
    public float forwardForce = 6f;   // ��������������
    public float upwardForce = 8f;    // ��ֱ���ϵ���

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ����������ķ��򣨵�λ������
        Vector3 toCamera = (Camera.main.transform.position - transform.position).normalized;

        // ȥ��Y�������������·ɣ������Լ��������׵ĸ߶ȣ�
        toCamera.y = 0f;
        toCamera.Normalize();

        // �ϳ����䷽��
        Vector3 throwDir = toCamera * forwardForce + Vector3.up * upwardForce;

        // ��������
        rb.AddForce(throwDir, ForceMode.Impulse);

        SFXManager.sfxManager.PlayFallingScream();
    }

    void Update()
    {
        // Y����ת
        transform.Rotate(Vector3.up * 180f * Time.deltaTime, Space.World);

        // ��������������ɳ���Ļ���̫�ͣ�
        if (/*transform.position.y < -10f*/IsOffScreen(transform.position))
        {
            Debug.Log("En");
            Destroy(gameObject);
        }
    }

    public bool IsOffScreen(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // �������������棨Z<0�����͵����Ѿ�������Ļ��
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
