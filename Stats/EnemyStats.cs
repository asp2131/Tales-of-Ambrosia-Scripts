using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public GameObject deathEffect;

    public Stat experienceAmount;

    public void GiveExperience()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.GainExperience(experienceAmount.GetValue());
    }

    public override void Die()
    {
        //make rigid body of particles fall to the ground
        base.Die();
        ///wait 3 seconds before destroying the enemy
        GiveExperience();

        //this is so the death animation can play
        GetComponent<Animator>()
            .SetTrigger("Die");
        var deathParticles =
            Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            print("Enemy got touched by " + collision.gameObject.name);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GetComponent<Animator>().SetTrigger("Hurt");
    }
}
