using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private GameObject _cloudPrefab;

    public int numClouds;

    void Start()
    {
        for (int i = 5; i < numClouds + 5; i++)
        {
            // Spawn a cloud every other Y unit
            Vector3 spawnPos = new(0, i + 1, 0);

            // Randomly pick to spawn the cloud starting on the left or right
            int rand = Random.Range(0, 2);
            if (rand == 0) spawnPos.x = Camera.main.ViewportToWorldPoint(Vector3.zero).x; // Set x to far left
            else if (rand == 1) spawnPos.x = Camera.main.ViewportToWorldPoint(Vector3.one).x; // Set x to far right
            
            // Instantiate cloud
            Instantiate(_cloudPrefab, spawnPos, Quaternion.identity);
        }
    }
}