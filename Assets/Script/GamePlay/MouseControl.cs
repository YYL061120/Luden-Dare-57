using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MouseControl : MonoBehaviour
{
    public static MouseControl mouseControl;
    public float smoothSpeed = 5f;
    private Camera mainCamera;
    public LayerMask raycastLayerMask;
    public GameObject PeopleSprite;

    [Header("Scroll")]

    public float scrollSensitivity = 10f;
    public float smoothTime = 0.2f;
    public float minY = -10f;
    public float maxY = 10f;
    private float targetY;
    private float velocityY = 0f;

    private Vector3 initialSize = new Vector3(1, 1, 1);
    private Vector3 clickSize = new Vector3(0.75f, 0.75f, 0.75f);
    private Vector3 ppSize = new Vector3(1.55f, 1.55f, 1.55f);


    void Start()
    {
        transform.localScale = initialSize;
        mouseControl = this;
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        targetY = mainCamera.transform.position.y;
    }

    void Update()
    {
        Vector3 target = GetMouseWorldPoint();
        movingCame();
        transform.position = Vector3.Lerp(transform.position, target, smoothSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            transform.localScale = clickSize;
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            transform.localScale = ppSize;
            GeneratePP();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            transform.localScale = initialSize;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            transform.localScale = initialSize;
        }
    }
    public void GeneratePP()
    {
        if (PeopleSprite != null)
        {
            GameObject pp = Instantiate(PeopleSprite, transform.position, Quaternion.Euler(0f, 35f, 0f));
            pp.GetComponent<Rigidbody>().AddForce(Vector3.up*80f,ForceMode.Impulse);
            Destroy(pp, 1.2f);
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
