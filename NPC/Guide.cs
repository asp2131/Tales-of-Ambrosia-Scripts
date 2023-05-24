using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : Interactable
{
    // Start is called before the first frame update
    Transform target; // Reference to the player

    public float lookRadius = 5f; // Detection range for player

    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            FaceTarget(); // Make sure to face towards the target
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * 5f
        );
    }
}
