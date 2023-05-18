using UnityEngine;

[RequireComponent(typeof(Transform))]
public class SpawnObjects : MonoBehaviour
{
    [Tooltip("The prefab to spawn.")]
    public GameObject prefab;

    [Tooltip("The minimum number of copies to spawn.")]
    public int minCount = 50;

    [Tooltip("The maximum number of copies to spawn.")]
    public int maxCount = 100;

    [Tooltip("The range in which to spawn the copies.")]
    public Vector3 spawnRange = new Vector3(10f, 0f, 10f);

    private void Start()
    {
        // Generate a random number of copies to spawn.
        int count = Random.Range(minCount, maxCount);

        // Spawn the copies within the specified range.
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y), Random.Range(-spawnRange.z, spawnRange.z));
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}