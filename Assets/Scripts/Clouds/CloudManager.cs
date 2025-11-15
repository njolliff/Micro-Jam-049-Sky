using UnityEngine;

public class CloudManager : MonoBehaviour
{
    #region Variables
    // SERIALIZED
    [Header("References")]
    [SerializeField] private Transform _cloudContainer;
    [SerializeField] private LootTable _batteryLootTable;
    [SerializeField] private CloudLootTable _cloudLootTable;

    [Header("Spawning")]
    [Tooltip("How far apart clouds should be spaced")]
    public float cloudDistance;

    [Tooltip("The odds for each cloud to spawn a battery."), Range(0,100)]
    public float baseBatteryChance;
    
    [Tooltip("The maximum number of battery-less clouds that can be spawned before a battery is guaranteed to spawn.")]
    public int guaranteeBatteryBy;

    // NON-SERIALIZED
    private float highestY = 0;
    private int numCloudsSinceLastBattery = 0;
    #endregion

    void Update()
    {
        // Check if new highest Y reached
        if (transform.position.y >= highestY + cloudDistance)
        {
            highestY += cloudDistance; // Increment highestY

            // Spawn Pos Y = highestY
            Vector3 spawnPos = new(0, highestY, 0);

            // Spawn Pos X is a random point within the screen width
            float randX = Random.Range(0f, 100f); // Get random float from 1-100 for variance
            if (randX != 0) randX /= 100; // Divide by 100 (if not 0) to put between 0-1 for viewport value
            spawnPos.x = Camera.main.ViewportToWorldPoint(new Vector3(randX, 0, 0)).x; // Convert to world coordinates and set spawn pos X

            // Spawn cloud
            SpawnCloud(spawnPos);
        }
    }

    private void SpawnCloud(Vector3 spawnPos)
    {
        // Pick a type of cloud
        GameObject cloudPrefab = _cloudLootTable.GetRandomEntry();

        // Check if a battery should be spawned
        GameObject batteryPrefab = null; // Default battery prefab to null
        float rand = Random.Range(0f, 100f);
        if (rand <= baseBatteryChance || numCloudsSinceLastBattery >= guaranteeBatteryBy)
        {
            numCloudsSinceLastBattery = 0; // Reset battery-less cloud counter
            batteryPrefab = _batteryLootTable.GetRandomEntry(); // Get random battery type
        }

        // Instantiate cloud and battery (if appropriate)
        Cloud cloud = Instantiate(cloudPrefab, spawnPos, Quaternion.identity, _cloudContainer).GetComponent<Cloud>();
        if (batteryPrefab != null) cloud.SpawnBattery(batteryPrefab);
    }
}