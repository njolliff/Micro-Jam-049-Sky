using UnityEngine;

[CreateAssetMenu(fileName = "CloudTable", menuName = "Scriptable Objects/Loot Tables/Cloud Table")]
public class CloudLootTable : ScriptableObject
{

    [Header("Table")]
    public CloudTableEntry[] entries;

    [Header("References")]
    public LootTable bouncyCloudTable;
    public GameObject normalCloudPrefab;
    public GameObject fastCloudPrefab;
    public GameObject slowCloudPrefab;


    public GameObject GetRandomEntry()
    {
        // Get total weight
        float totalWeight = 0f;
        foreach (CloudTableEntry entry in entries)
            totalWeight += entry.weight;

        // Get random number in range of total weight
        float random = Random.Range(0, totalWeight);

        // Determine the appropriate cloud entry
        foreach (CloudTableEntry entry in entries)
        {
            // Appropriate entry found
            if (random < entry.weight)
            {
                // For any type other than bouncy, return the prefab
                // For bouncy, return a variant from the bouncy cloud loot table
                switch (entry.cloudType)
                {
                    case CloudTableEntry.CloudType.Normal:
                        return normalCloudPrefab;

                    case CloudTableEntry.CloudType.Fast:
                        return fastCloudPrefab;

                    case CloudTableEntry.CloudType.Slow:
                        return slowCloudPrefab;

                    case CloudTableEntry.CloudType.Bouncy:
                        return bouncyCloudTable.GetRandomEntry();
                }
            }

            // Not an appropriate entry
            else
                random -= entry.weight;
        }

        // Default case, should never be reached if there are entries in the table
        return null;
    }
}