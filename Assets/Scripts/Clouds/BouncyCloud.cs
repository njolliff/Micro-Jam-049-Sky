using UnityEngine;

public class BouncyCloud : Cloud
{
    public float bounceStrength = 10f;

    private bool _canBounce = true;
    private float _bounceCD = 0.1f;
    private float _bounceCDTimer = 0;

    void Update()
    {
        MoveCloud();

        // Handle bounce CD
        if (!_canBounce)
        {
            _bounceCDTimer += Time.deltaTime;
            if (_bounceCDTimer >= _bounceCD)
            {
                _canBounce = true; // Enable bouncing
                _bounceCDTimer = 0; // Reset timer
            }
        }
    }

    private bool IsPlayerOnTop(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y < 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _canBounce)
        {
            // Get rigid body
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            
            // Only apply bounce if player is falling from above
            if (playerRB != null && playerRB.linearVelocityY <= 0 && IsPlayerOnTop(collision))
            {
                // Halt player Y velocity then apply force so that bounce strength is always the same
                playerRB.linearVelocityY = 0;
                playerRB.AddForceY(bounceStrength, ForceMode2D.Impulse);

                // Disable bounce
                _canBounce = false;
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _canBounce)
        {
            // Get rigid body
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();

            // Only apply bounce if player is falling from above
            if (playerRB != null && playerRB.linearVelocityY <= 0 && IsPlayerOnTop(collision))
            {
                // Halt player Y velocity then apply force so that bounce strength is always the same
                playerRB.linearVelocityY = 0;
                playerRB.AddForceY(bounceStrength, ForceMode2D.Impulse);

                // Disable bounce
                _canBounce = false;
            }
        }
    }
}