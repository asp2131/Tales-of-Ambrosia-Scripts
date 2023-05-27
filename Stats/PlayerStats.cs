using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public int experience;

    public int maxExperience = 100;

    public int level = 1;

    private PlayerAnimator animator;

    public GameObject levelUpUI;

    public event System.Action<float, float> OnExperienceChanged;

    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        animator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (OnExperienceChanged != null)
        {
            OnExperienceChanged(maxExperience, experience);
        }
        //if experience is greater than max experience
        if (experience >= maxExperience)
        {
            //subtract max experience from experience
            experience -= maxExperience;
            //increase max experience by 10
            maxExperience += 10;

            //increase level by 1
            LevelUp();
        }

        if (Time.time - lastDamageTime > regenerationDelay)
        {
            RegenerateHealth();
        }
    }

    public void LevelUp()
    {
        //Access CharacterAnimator from Player Instance
        animator.OnLevelUp();
        //increase level by 1
        level++;
        //increase max health by 10
        maxHealth += 10;
        //increase max mana by 10
        // maxMana += 10;
        //increase max stamina by 10
        // maxStamina += 10;
        //increase damage by 1
        damage.AddModifier(1);
        //increase armor by 1
        armor.AddModifier(1);

        //translate rect transform of level up UI x by 400
        levelUpUI.GetComponent<RectTransform>().Translate(400, 0, 0);
        //access header game object from level up UI
        GameObject header = levelUpUI.transform.GetChild(1).gameObject;
        //access TextMeshPro - Text from header
        TMPro.TextMeshProUGUI headerText = header.GetComponent<TMPro.TextMeshProUGUI>();
        //set text to "Level Up!"
        headerText.text = "Level Up!";
        GameObject main = levelUpUI.transform.GetChild(2).gameObject;
        //access TextMeshPro - Text from header
        TMPro.TextMeshProUGUI mainText = main.GetComponent<TMPro.TextMeshProUGUI>();
        //set text to "Level Up!"
        headerText.text = "You've reached level " + level + " !";

        //after three seconds translate rect transform of level up UI x by -400
        StartCoroutine(ResetLevelUpUI());
    }

    IEnumerator ResetLevelUpUI()
    {
        yield return new WaitForSeconds(3);
        levelUpUI.GetComponent<RectTransform>().Translate(-400, 0, 0);
    }

    public void GainExperience(int amount)
    {
        //increase experience by amount
        experience += amount;
    }

    // Update is called once per frame
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        // Kill the player
        PlayerManager.instance.KillPlayer();
    }
}
