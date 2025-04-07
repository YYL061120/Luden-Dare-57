using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class concreteFactory : MonoBehaviour
{
    public GameObject hitEffectPrefab;
    public GameObject destroyEffectPrefab;
    public bool canManufacture = false;

    public float flashDuration = 0.5f;
    public float fadeDuration = 1.0f;

    private Renderer rend;
    private Color originalColor;

    public GameObject stoneface;
    private StoneFaceMoveEat ST;

    [Header("Occupacy and Health")]
    public int maxOccupacy;
    public int currentPeople;
    public float maxHealth;
    public float currentHealth;

    [Header("Efficiency")]
    public int currentEfficiency = 5;

    public Vector3 initalPos;
    public Vector3 initalScale;
    public Boolean ismousecollider;
    private void Awake()
    {

    }

    private void Start()
    {
        initalPos = transform.position;
        initalScale = transform.localScale;
        ismousecollider = false;
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        StartCoroutine(Manufacturing());
    }

    private void Update()
    {
        AddPeople();
    }
    private void FixedUpdate()
    {
        
        if (stoneface != null)
        {
            if (stoneface.activeSelf == true)
            {
                ST = GameObject.Find("StoneFace").GetComponent<StoneFaceMoveEat>();
            }
        }
        //Debug.Log("Concrete: "+manager.currentConcreteCount);
        if (currentHealth <= 0)
        {
            ST.facilitiesList.Remove(this.gameObject);
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "StoneFace")
        {
            stoneface = collision.gameObject;
        }
        if (collision.transform.tag == "Incarnation")
        {
            ismousecollider = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Incarnation")
        {
            ismousecollider = false;
        }
    }


    public IEnumerator Manufacturing()
    {
        int efficiency = 20;
        while (true)
        {
            if (currentPeople > 0) canManufacture = true;
            else canManufacture = false;
            if (canManufacture)
            {
                int addValue = currentEfficiency + currentPeople; //5材料/20s（+1材料/20s）
                GameManager.gameManager.currentConcreteCount += addValue;
            }
            yield return new WaitForSeconds(efficiency);
        }
    }

    public void HealthDeductingEffect()
    {
        StartCoroutine(FlashToRedAndBack());
        Vector3 hitPos = transform.position - new Vector3(1, 0, 2);
        Instantiate(hitEffectPrefab, hitPos, Quaternion.Euler(-90, 0, 0));
    }

    public void AddPeople()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (ismousecollider)
            {
                if (currentPeople < maxOccupacy)
                {
                    if (GameManager.gameManager.currentPeopleCount>0)
                    {
                        currentPeople++;
                        GameManager.gameManager.currentPeopleCount--;
                        transform.DOShakePosition(0.15f, 0.15f, 40).OnComplete(() => transform.position = initalPos);
                        transform.DOShakeScale(0.15f, 0.3f, 30).OnComplete(() => transform.localScale = initalScale);
                    }
                    
                }
            }
            
        }
        
    }

    private IEnumerator FlashToRedAndBack()
    {
        // 获取所有子物体的 SpriteRenderer
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        // 保存原来的颜色
        Dictionary<SpriteRenderer, Color> originalColors = new Dictionary<SpriteRenderer, Color>();
        foreach (var renderer in renderers)
        {
            originalColors[renderer] = renderer.color;
            renderer.color = Color.red; // 立刻变红
        }

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            foreach (var renderer in renderers)
            {
                if (renderer != null && originalColors.ContainsKey(renderer))
                {
                    renderer.color = Color.Lerp(Color.red, originalColors[renderer], t);
                }
            }

            yield return null;
        }

        // 保证最后回到原色
        foreach (var renderer in renderers)
        {
            if (renderer != null && originalColors.ContainsKey(renderer))
            {
                renderer.color = originalColors[renderer];
            }
        }
    }
}
