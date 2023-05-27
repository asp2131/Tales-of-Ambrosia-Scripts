using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : Interactable
{
    // Start is called before the first frame update
    Transform target; // Reference to the player

    DialogManager dialogManager;

    public float lookRadius = 5f; // Detection range for player

    public string[] dialog = new string[2];

    public Quest quest;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        dialogManager = DialogManager.instance;
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

        if (isFocus == true)
        {
            StartConversation();
        }
        else
        {
            dialogManager.dialogIndex = 0;
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

    void StartConversation()
    {
        //if player is interacting with NPC
        //display dialogue
        //create string array of dialogue


        //pass string array to dialogManager


        dialogManager.ShowDialog(transform.name, dialog);
    }
}
