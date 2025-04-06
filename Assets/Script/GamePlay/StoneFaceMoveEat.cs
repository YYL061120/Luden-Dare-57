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
        // ��ȡ�����������֮��ķ���
        Vector3 direction = -mainCamera.transform.position - transform.position;

        // ������� Y �� Z ����ԣ�ֻ��ת������ X �� Z ƽ����
        direction.y = 0; // ���� Y ����ת��ȷ�����岻����б��

        // ��ת���壬ʹ��ʼ�����������
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
}
