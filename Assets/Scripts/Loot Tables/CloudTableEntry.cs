using UnityEngine;

[System.Serializable]
public class CloudTableEntry
{
    public enum CloudType { Normal, Fast, Slow, Bouncy }
    public CloudType cloudType;

    [Range(0, 100)]
    public float weight;
}