using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceUI : MonoBehaviour
{
    public GameObject uiPrefab;

    Transform ui;
    Image expSlider;

    // Start is called before the first frame update
    void Start()
    {
        expSlider = uiPrefab.GetComponent<Image>();
        GetComponent<PlayerStats>().OnExperienceChanged += OnExperienceChanged;
    }

    void OnExperienceChanged(float maxExp, float currentExp)
    {
        // ui.gameObject.SetActive(true);
        // lastMadeVisibleTime = Time.time;

        float expPercent = (float)currentExp / maxExp;
        expSlider.fillAmount = expPercent;
        // if (currentHealth <= 0)
        // {
        //     Destroy(ui.gameObject);
        // }
    }

    // Update is called once per frame
    void Update() { }
}
