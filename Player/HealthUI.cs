using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    Transform ui;
    Image healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        //get Image component from game object
        healthSlider = uiPrefab.GetComponent<Image>();
        print("Health Slider: " + healthSlider);

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        float healthPercent = (float)currentHealth / maxHealth;
        healthSlider.fillAmount = healthPercent;
    }
}
