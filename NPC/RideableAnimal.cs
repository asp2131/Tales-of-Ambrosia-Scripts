using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimal : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    Transform target; // Reference to the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if target is on top of the animal
        if (Vector3.Distance(target.position, transform.position) <= 1f)
        {
            anim.SetFloat("Speed", 1);
            //move forward
            rb.velocity = transform.forward * 2f;
        }
        else
        {
            anim.SetFloat("Speed", 0);
            //stop moving
            rb.velocity = Vector3.zero;
        }
    }
}
