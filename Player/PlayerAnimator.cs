using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDictionary;

    // void Awake()
    // {
    //     defaultAnimationClips = weaponAnimations[0];
    // }

    protected override void Start()
    {
        base.Start();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationsDictionary = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationsDictionary.Add(a.weapon, a.clips);
        }
    }

    protected override void OnAttack()
    {
        base.OnAttack();
        animator.SetTrigger("Attack");
        int attackIndex = Random.Range(0, currentAnimationClips.Length);
        // loop through all the replaceable animations
        for (int i = 0; i < replaceableAnimations.Length; i++)
        {
            // if the current replaceable animation is the same as the one we are looking for

            // set the override controller to the current animation clip
            overrideController[replaceableAnimations[i].name] = currentAnimationClips[attackIndex];
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            if (weaponAnimationsDictionary.ContainsKey(newItem))
            {
                currentAnimationClips = weaponAnimationsDictionary[newItem];
            }
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            currentAnimationClips = defaultAnimationClips;
        }
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
