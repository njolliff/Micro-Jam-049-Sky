using UnityEngine;

public class Battery : MonoBehaviour
{
    public float batteryEnergy;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Player entered trigger
        if (collision.gameObject.CompareTag("Player"))
        {
            // Give player energy
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null) playerScript.energy += batteryEnergy;

            // Destroy battery
            Destroy(gameObject);
        }
    }
}