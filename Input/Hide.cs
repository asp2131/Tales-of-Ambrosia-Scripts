using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    // Update is called once per frame
    public void ToggleHide()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
