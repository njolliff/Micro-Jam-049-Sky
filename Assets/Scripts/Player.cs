using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] GameObject _gameOverScreen;

    [Header("Movement")]
    public float energy = 100f;
    public bool unlimitedEnergy = false;
    [SerializeField] private float _boostStrength = 1f, _energyDrainSpeed = 1f, _tiltStrength = 1f, _maxTorque = 0.2f;

    public int currentHeight => (int)transform.position.y;

    private float _movementInput = 0;
    private bool _isBoosting = false, _canMove = true;
    #endregion

    #region Update
    void FixedUpdate()
    {
        if (_canMove)
        {
            // Apply torque based on movement input (sign-flipped because positive = counter-clockwise)
            _rb.AddTorque(_tiltStrength * _movementInput * -1, ForceMode2D.Force);
            _rb.angularVelocity = Mathf.Clamp(_rb.angularVelocity, -_maxTorque, _maxTorque); // Clamp angular velocity so the force doesn't ramp up and spin like crazy

            // Apply boost if boosting
            if (_isBoosting) HandleBoost();
        }
    }
    #endregion

    #region Helper Methods
    private void HandleBoost()
    {
        // Ensure player has energy / infinity energy is enabled
        if (energy > 0 || unlimitedEnergy)
        {
            // Set parent to null for clean takeoffs on platforms
            transform.SetParent(null);

            // Apply force
            _rb.AddForce(transform.up * _boostStrength, ForceMode2D.Force);

            // Drain energy unless unlimited energy is enabled, setting it to 0 if it would be negative
            if (!unlimitedEnergy)
            {
                float updatedenergy = energy - (_energyDrainSpeed * Time.fixedDeltaTime);
                if (updatedenergy < 0) energy = 0;
                else energy = updatedenergy;   
            }
        }
    }
    private void GameOver()
    {
        // Pause game
        Time.timeScale = 0;

        // Enable game over screen
        _gameOverScreen.SetActive(true);
    }
    #endregion

    #region Input Methods
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Get input
        _movementInput = ctx.ReadValue<Vector2>().x;
    }
    public void OnBoost(InputAction.CallbackContext ctx)
    {
        // Enable boosting when the button is pressed and disable it when it is released
        if (ctx.performed) _isBoosting = true;
        if (ctx.canceled) _isBoosting = false;
    }
    #endregion

    #region Collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            // Parent player to platform so they move with the platform, but only if colliding from above
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    transform.SetParent(collision.transform);

                    // If grounded with no energy, player lost
                    if (energy <= 0)
                        GameOver();
                }
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // If grounded with no energy, player lost
            if (energy <= 0)
                GameOver();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            transform.SetParent(null);
    }
    #endregion
}