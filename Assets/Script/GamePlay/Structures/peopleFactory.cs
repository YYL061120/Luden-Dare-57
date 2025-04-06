using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peopleFactory : MonoBehaviour
{
    public bool canManufacture;
    public GameObject peopleProduced;

    public float flashDuration = 0.5f;
    public float fadeDuration = 1.0f;

    private Renderer rend;
    private Color originalColor;

    [Header("Occupacy and Health")]
    public int maxOccupacy;
    public int currentPeople;
    public float maxHealth;
    public float currentHealth;

    private void Awake()
    {

    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        StartCoroutine(Manufacturing());
        currentHealth = maxHealth;
    }
    private void Update()
    {
        //Debug.Log("People: "+manager.resource.peopleCount);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Manufacturing()
    {
        int efficiency = 60;
        while (true)
        {
            if (currentPeople > 0) canManufacture = true;
            else canManufacture = false;
            if (canManufacture)
            {
                GameManager.gameManager.currentPeopleCount++;
                PeoplePopOutEffect();   
            }
            float waitingTime = efficiency - currentPeople * 3f; //formula: 60s/人（-3s/人）
            yield return new WaitForSeconds(waitingTime);
        }

    }

    public void HealthDeduction()
    {
        StopAllCoroutines();
        StartCoroutine(FlashToRedAndBack());
    }

    public void PeoplePopOutEffect()
    {
        Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        Vector3 spawnPos = transform.position + offset;
        Quaternion randomRot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

        Instantiate(peopleProduced, spawnPos, randomRot);
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
