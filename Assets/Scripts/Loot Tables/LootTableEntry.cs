using UnityEngine;

[System.Serializable]
public class LootTableEntry
{
    public GameObject prefab;

    [Range(0, 100)]
    public float weight;
}