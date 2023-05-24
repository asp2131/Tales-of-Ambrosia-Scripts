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
        // healthSlider = uiPrefab.GetComponentInChildren<Image>();
        // print(healthSlider);
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        float healthPercent = (float)currentHealth / maxHealth;
        float heartMultiplier = 4f;
        float healthLoss = 1f - healthPercent;

        //create heart health system with uiPrefab's children
        for (int i = uiPrefab.transform.childCount - 1; i >= 0; i--)
        {
            //access last child of uiPrefab
            healthSlider = uiPrefab.transform.GetChild(i).GetComponent<Image>();
            // print(healthSlider);
            // print(healthSlider.fillAmount);
            // print(healthLoss);
            // print(healthLoss * heartMultiplier);
            // print(healthSlider.fillAmount - healthLoss * heartMultiplier);
            if (healthLoss <= 1f / uiPrefab.transform.childCount)
            {
                healthSlider.fillAmount = healthSlider.fillAmount - healthLoss * heartMultiplier;
                // print(healthSlider.fillAmount);
                break;
            }
            else
            {
                healthSlider.fillAmount = 0f;
                healthLoss = healthLoss - 1f / uiPrefab.transform.childCount;
                // print(healthSlider.fillAmount);
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
