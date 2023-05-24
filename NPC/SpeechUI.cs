using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechUI : MonoBehaviour
{
    public GameObject speechBubblePrefab;
    public Transform target;

    Transform player;

    public float lookRadius = 5f; // Detection range for player

    Transform ui;
    Image healthSlider;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            print("1");
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(speechBubblePrefab, canvas.transform).transform;
                ui.gameObject.SetActive(false);
                break;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -Camera.main.transform.forward;
        }
        float distance = Vector3.Distance(target.position, player.position);
        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            print("in range");
            ui.gameObject.SetActive(true);
        }
        else
        {
            ui.gameObject.SetActive(false);
        }
    }
}
