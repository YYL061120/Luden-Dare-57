using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image image;

    private void Awake()
    {
        slider = transform.GetComponent<Slider>();
        image = gameObject.GetComponentInChildren<Image>();
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        image.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(float max)
    {
        slider.maxValue = max;
        slider.value = max;
        image.color = gradient.Evaluate(1f);
    }
}
