using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    private Rigidbody rb;
    private float time = 0f;
    private Vector3 startPosition;
    public float speed = 1f;
    public float distance = 1f;
    public float height = 1f;
    public float frequency = 1f;
    public float randomOffset = 0.5f;
    public float randomFrequency = 0.5f;
    public float randomHeight = 0.5f;
    public float randomSpeed = 0.5f;
    public float randomDistance = 0.5f;
    public float randomAngle = 0.5f;
    public float randomAngleFrequency = 0.5f;
    public float randomAngleHeight = 0.5f;
    public float randomAngleDistance = 0.5f;
    public float randomAngleOffset = 0.5f;
    public float randomAngleSpeed = 0.5f;
    private float angle = 0f;
    private float angleSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        angle = Random.Range(0f, 360f);
        angleSpeed = Random.Range(-randomAngleSpeed, randomAngleSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //move the butterfly
        time += Time.deltaTime * speed;
        float x = Mathf.Sin(time * frequency) * distance;
        float y = Mathf.Sin(time * frequency) * height;
        float z = Mathf.Cos(time * frequency) * distance;
        Vector3 newPosition = new Vector3(x, y, z);
        newPosition += startPosition;
        rb.MovePosition(newPosition);

        //rotate the butterfly
        angle += angleSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
