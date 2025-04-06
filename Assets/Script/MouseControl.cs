using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MouseControl : MonoBehaviour
{
    public float smoothSpeed = 5f;
    private Camera mainCamera;
    public LayerMask raycastLayerMask;

    [Header("Scroll")]

    public float scrollSensitivity = 10f;
    public float smoothTime = 0.2f;
    public float minY = -10f;
    public float maxY = 10f;
    private float targetY;
    private float velocityY = 0f;




    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        targetY = mainCamera.transform.position.y;
    }

    void Update()
    {
        Vector3 target = GetMouseWorldPoint();
        movingCame();
        transform.position = Vector3.Lerp(transform.position, target, smoothSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Cursor.visible = false;
        }
    }

    Vector3 GetMouseWorldPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayerMask))
        {
            return hit.point;
            Cursor.visible = false;
        }
        return transform.position;
    }

    public void movingCame()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollInput) > 0.01f)
        {
            targetY += scrollInput * scrollSensitivity;
            targetY = Mathf.Clamp(targetY, minY, maxY);
        }
        float newY = Mathf.SmoothDamp(mainCamera.transform.position.y, targetY, ref velocityY, smoothTime);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, newY, mainCamera.transform.position.z);
    }
}
