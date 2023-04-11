using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    public float attackCooldown = 0f;
    public float combatCooldown = 5;

    public float projectileSpeed = 10f;
    public float lastAttackTime;

    public event System.Action OnAttack;

    public bool InCombat { get; private set; }

    public Camera cam;

    public GameObject projectile;

    public Transform LHFirepoint,
        RHFirepoint;

    private Vector3 destination;

    private bool leftHand;
    CharacterStats myStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            // targetStats.TakeDamage(myStats.damage.GetValue());
            if (OnAttack != null)
            {
                print("OnAttack not null");
                OnAttack();
                ShootProjectile();
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
        //get reference to enemy position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Debug.Log("We hit " + hit.collider.name + " " + hit.point);
            // Move our player to what we hit with our character controller

            //Check to see if we click an interactable
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                destination = interactable.transform.position;
            }
        }

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
