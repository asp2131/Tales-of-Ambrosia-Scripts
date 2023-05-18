using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy(gameObject);
            CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
            targetStats.TakeDamage(myStats.damage.GetValue());
        }
    }
}
