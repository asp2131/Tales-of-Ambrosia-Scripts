using UnityEngine;

[RequireComponent(typeof(Transform))]
public class SpawnObjects : MonoBehaviour
{
    [Tooltip("The prefab to spawn.")]
    //import list of prefabs
    public GameObject[] prefabs;

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

        // Spawn the copies of the prefabs in list of prefabs.
        for (int i = 0; i < count; i++)
        {
            // Generate a random position to spawn the copy at.
            Vector3 position = transform.position;
            position.x += Random.Range(-spawnRange.x, spawnRange.x);
            position.y += Random.Range(-spawnRange.y, spawnRange.y);
            position.z += Random.Range(-spawnRange.z, spawnRange.z);

            // Generate a random rotation to spawn the copy with.
            Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            // Spawn the copy.
            //Instantiate(prefab, position, rotation);
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, rotation);
        }
    }
}
