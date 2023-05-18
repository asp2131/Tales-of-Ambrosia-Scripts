using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    public float attackCooldown = 0f;
    public float combatCooldown = 5;

    public float projectileSpeed = 10f;
    public float lastAttackTime;

    public event System.Action OnMagicAttack;

    public event System.Action OnSwordAttack;

    public bool InCombat { get; private set; }

    public Camera cam;

    public GameObject projectile;

    public Transform LHFirepoint,
        RHFirepoint;

    private Vector3 destination;

    private bool leftHand;
    CharacterStats myStats;

    public PlayerInput playerInput;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
        // if (playerInput.actions["Fire"].ReadValue<float>() > 0)
        // {
        //     OnMagicAttack();
        //     ShootProjectile();
        // }
        if (attackCooldown <= 0f)
        {
            if (playerInput.actions["Fire"].ReadValue<float>() > 0)
            {
                OnMagicAttack();
                ShootProjectile();
                attackCooldown = 0.3f / attackSpeed;
                lastAttackTime = Time.time;
            }
            if (playerInput.actions["Sword"].ReadValue<float>() > 0)
            {
                OnSwordAttack();
                attackCooldown = 1f / attackSpeed;
                lastAttackTime = Time.time;
            }
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            targetStats.TakeDamage(myStats.damage.GetValue());
            if (OnMagicAttack != null)
            {
                OnSwordAttack();
                // ShootProjectile();
            }
            if (targetStats.currentHealth <= 0)
            {
                InCombat = false;
            }

            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    void ShootProjectile()
    {
        //make destination straight forward from player
        //if the platform is android
        // if (Application.platform == RuntimePlatform.Android)
        // {
        // }
        destination = transform.position + transform.forward * 10;

        // else
        // {
        //     Ray ray;
        //     //if using android
        //     if (Application.platform == RuntimePlatform.Android)
        //     {
        //         ray = cam.ScreenPointToRay(Input.GetTouch(0).position);
        //     }
        //     else
        //     {
        //         ray = cam.ScreenPointToRay(Input.mousePosition);
        //     }

        //     RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         // Debug.Log("We hit " + hit.collider.name + " " + hit.point);
        //         // Move our player to what we hit with our character controller

        //         //Check to see if we click an interactable
        //         Interactable interactable = hit.collider.GetComponent<Interactable>();
        //         if (interactable != null)
        //         {
        //             destination = interactable.transform.position;
        //         }
        //     }
        // }
        if (leftHand)
        {
            leftHand = false;
            InstantiateProjectile(LHFirepoint);
        }
        else
        {
            leftHand = true;
            InstantiateProjectile(RHFirepoint);
        }
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj =
            Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;

        //get rigidbody of projectile
        var projectileRb = projectileObj.GetComponent<Rigidbody>().velocity =
            (destination - firePoint.position).normalized * projectileSpeed;
    }
}
