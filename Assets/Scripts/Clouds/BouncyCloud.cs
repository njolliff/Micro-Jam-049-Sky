using UnityEngine;

public class BouncyCloud : Cloud
{
    public float bounceStrength = 10f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get rigid body
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRB != null)
            {
                // Only apply bounce if player is falling
                if (playerRB.linearVelocityY <= 0)
                {
                    // Halt player Y velocity then apply force so that bounce strength is always the same
                    playerRB.linearVelocityY = 0;
                    playerRB.AddForceY(bounceStrength, ForceMode2D.Impulse);
                }
            }
        }
    }
}