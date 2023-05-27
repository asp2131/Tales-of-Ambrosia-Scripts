using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideableAnimal : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    private CharacterController controller;

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
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if target is on top of the animal
        if (Vector3.Distance(target.position, transform.position) <= 1f)
        {
            anim.SetFloat("Speed", 1);
            //move forward at a constant speed with controller
            controller.Move(transform.forward * 2f * Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0);
            //stop moving
            rb.velocity = Vector3.zero;
        }
    }
}
