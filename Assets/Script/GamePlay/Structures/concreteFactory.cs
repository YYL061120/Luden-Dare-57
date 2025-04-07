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

    private void Awake()
    {

    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        StartCoroutine(Manufacturing());
    }
    private void Update()
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
        if (currentPeople < maxOccupacy)
        {
            currentPeople++;
            GameManager.gameManager.currentPeopleCount--;
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
