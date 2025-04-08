using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StoneFaceMoveEat : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody stRB;
    public List<GameObject> facilitiesList = new List<GameObject>();

    [Header("Increasing Parameters")]
    public float damage;
    public float movingSpeed;
    public float eatingFrequency = 5f;
    public Sprite[] Fs;
    public int frame;

    public bool isMovingState = true;
    public bool isDealingDamage = false;
    private Coroutine eatDelayCoroutine;
    public float NextFrameCD;
    private float CurrentFCD;
    public SpriteRenderer Face;
    public bool isPlayingEating = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        stRB = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        AdjustFacing();
        MovingOrEating();
        HumanRemains();
    }

    private void FixedUpdate()
    {

    }
    public void AdjustFacing()
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Facilities")
        {
            isMovingState = false;
            facilitiesList.Add(collision.gameObject);
        }
        if(collision.gameObject.tag == "TheDrill")
        {
            changescreen.Changescreen.changeSceneToDefeat();
        }

        }
    public void MovingOrEating()
    {
        if (isMovingState)
        {
            //StopCoroutine(DealDamage());
            transform.Translate(Vector3.down * movingSpeed * Time.deltaTime);
        }
        else
        {
            if (!facilitiesList.Any())
            {
                isDealingDamage = false;
                isMovingState = true;
            }
            if (!isDealingDamage)
            {
                StartCoroutine(DealDamage());
                isDealingDamage = true;
            }
        }
    }

    public IEnumerator DealDamage()
    {
        while (!isMovingState)
        {
            
            yield return new WaitForSeconds(eatingFrequency);
            foreach (GameObject facility in facilitiesList)
            {
                if (facility == null) continue;

                if (facility.GetComponent<ironFactory>() != null)
                {
                    facility.GetComponent<ironFactory>().currentHealth -= damage;
                    facility.GetComponent<ironFactory>().HealthDeduction();
                }
                else if (facility.GetComponent<concreteFactory>() != null)
                {
                    facility.GetComponent<concreteFactory>().currentHealth -= damage;
                    facility.GetComponent<concreteFactory>().HealthDeductingEffect();
                }
                else if (facility.GetComponent<peopleFactory>() != null)
                {
                    facility.GetComponent<peopleFactory>().currentHealth -= damage;
                    facility.GetComponent<peopleFactory>().HealthDeduction();
                }
                else if (facility.GetComponent<DefensiveStructure>() != null)
                {
                    facility.GetComponent<DefensiveStructure>().currentHealth -= damage;
                    facility.GetComponent<DefensiveStructure>().HealthDeduction();
                }
                damage += damage * 0.05f;
                eatingFrequency *= 0.99f;

            }
            if (isPlayingEating == false)
            {
                StartCoroutine(PlayBiteCoroutine());
            }

        }
        isDealingDamage = false;
    }

    public void MovingSpeedChanger()
    {
        //float time = GameManager.gameManager.playingDuration;
    }

    private IEnumerator PlayBiteCoroutine()
    {
        isPlayingEating = true;
        int framesToShow = 4;

        for (int frame = 0; frame < framesToShow; frame++)
        {
            Face.sprite = Fs[frame];
            yield return new WaitForSeconds(NextFrameCD);
        }
        Face.sprite = Fs[0];
        isPlayingEating = false;
    }


    public void HumanRemains()
    {
        if (GameManager.gameManager.currentPeopleCount <= 0 &&!lastHopeCheck.LastHopeCheck.isthereHope)
        {
            changescreen.Changescreen.changeSceneToDefeat();
        }
    }
}
