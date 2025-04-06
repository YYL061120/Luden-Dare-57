using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float smoothSpeed = 5f;
    private Camera mainCamera;
    public LayerMask raycastLayerMask;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 target = GetMouseWorldPoint();
        // Lerp toward the target using a smoothing factor.
        transform.position = Vector3.Lerp(transform.position, target, smoothSpeed * Time.deltaTime);
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
}
