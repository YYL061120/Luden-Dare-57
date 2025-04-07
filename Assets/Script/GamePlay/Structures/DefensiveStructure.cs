using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveStructure : MonoBehaviour 
{
    public healthBar healthBar;
    public GameObject hitEffectPrefab;
    public GameObject destroyEffectPrefab;
    public GameObject stoneface;
    public float maxHealth;
    public float currentHealth;

        public float flashDuration = 0.5f;
    public float fadeDuration = 1.0f;

    private Renderer rend;
    private Color originalColor;

    private StoneFaceMoveEat ST;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        stoneface = GameObject.FindGameObjectWithTag("StoneFace");
        healthBar.SetHealth(currentHealth);
        healthBar.SetMaxHealth(maxHealth);
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthBar.SetHealth(currentHealth);
        if (stoneface != null)
        {
            if (stoneface.activeSelf == true)
            {
                ST = GameObject.Find("StoneFace").GetComponent<StoneFaceMoveEat>();
            }
        }
        
        if (currentHealth <= 0)
        {
            ST.facilitiesList.Remove(this.gameObject);
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(this.gameObject);
        }
    }

    public void HealthDeduction()
    {
        Vector3 hitPos = transform.position - new Vector3(1,0,2);
        Instantiate(hitEffectPrefab, hitPos, Quaternion.Euler(-90, 0, 0));
        StartCoroutine(FlashThenFade());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag== "StoneFace")
        {
            stoneface = collision.gameObject;
        }
    }


    private IEnumerator FlashThenFade()
    {
        // Step 1: ������ɺ�ɫ
        rend.material.color = Color.red;

        // Step 2: ����һ��ʱ��
        yield return new WaitForSeconds(flashDuration);

        // Step 3: ��ɫ�����ȥ
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            rend.material.color = Color.Lerp(Color.red, originalColor, t);
            yield return null;
        }

        // ȷ�������ɫ�ص�ԭ��
        rend.material.color = originalColor;
    }
}
