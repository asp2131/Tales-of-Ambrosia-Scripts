using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactEffect;
    PlayerManager playerManager;

    CharacterStats myStats;

    CharacterStats targetStats;

    private void Start()
    {
        playerManager = PlayerManager.instance;
    }

    void OnCollisionEnter(Collision collision)
    {
        targetStats = collision.gameObject.GetComponent<CharacterStats>();
        myStats = playerManager.player.GetComponent<CharacterStats>();
        print("Projectile hit " + collision.gameObject);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            var impact =
                Instantiate(impactEffect, transform.position, Quaternion.identity) as GameObject;
            CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
            targetStats.TakeDamage(myStats.damage.GetValue());
        }
        //if 5 seconds pass, destroy the projectile
        Destroy(gameObject, 5f);
    }
}
