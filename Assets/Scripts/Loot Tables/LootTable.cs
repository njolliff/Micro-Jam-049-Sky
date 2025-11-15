using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Scriptable Objects/Loot Tables/Normal Table")]
public class LootTable : ScriptableObject
{
    public LootTableEntry[] entries;

    public GameObject GetRandomEntry()
    {
        // Get total weight
        float totalWeight = 0f;
        foreach (LootTableEntry entry in entries)
            totalWeight += entry.weight;

        // Get random number in range of total weight
        float random = Random.Range(0, totalWeight);

        // Return the appropriate entry
        foreach (LootTableEntry entry in entries)
        {
            if (random < entry.weight)
                return entry.prefab;
            else
                random -= entry.weight;
        }

        // Default case, should never be reached if there are entries in the table
        return null;
    }
}