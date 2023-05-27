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

    float lastHealthLossAmount;

    // Start is called before the first frame update
    void Start()
    {
        //get Image component from game object
        // healthSlider = uiPrefab.GetComponentInChildren<Image>();
        // print(healthSlider);
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        float healthPercent = (float)currentHealth / maxHealth;
        float heartMultiplier = 4f;
        float healthLoss = 1f - healthPercent;
        lastHealthLossAmount = healthLoss;

        //if health is full, set health slider to 1
        if (healthPercent == 1f)
        {
            for (int i = 0; i < uiPrefab.transform.childCount; i++)
            {
                healthSlider = uiPrefab.transform.GetChild(i).GetComponent<Image>();
                if (healthSlider.fillAmount != 1f)
                {
                    healthSlider.fillAmount += 5f;
                }
            }
        }

        //create heart health system with uiPrefab's children
        for (int i = uiPrefab.transform.childCount - 1; i >= 0; i--)
        {
            healthSlider = uiPrefab.transform.GetChild(i).GetComponent<Image>();

            if (healthLoss <= 1f / uiPrefab.transform.childCount)
            {
                healthSlider.fillAmount = healthSlider.fillAmount - healthLoss * heartMultiplier;
                break;
            }
            else
            {
                healthSlider.fillAmount = 0f;
                healthLoss = healthLoss - 1f / uiPrefab.transform.childCount;
            }
        }
    }
}


// if (healthLoss <= 1f / uiPrefab.childCount)
// {
//     print("health loss: " + healthLoss);
//     //access last child of uiPrefab
//     healthSlider = ;
//     healthSlider.fillAmount = healthSlider.fillAmount - healthLoss * heartMultiplier;
// }
