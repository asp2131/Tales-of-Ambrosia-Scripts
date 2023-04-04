using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            // Call ItemPickup pickup method
        }
    }

    void OnTriggerStay(Collider other) { }

    void OnTriggerExit(Collider other) { }
}
