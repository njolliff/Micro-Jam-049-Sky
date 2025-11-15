using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _collider;

    [Header("Movement")]
    public float energy = 100f;
    public bool unlimitedEnergy = false;
    [SerializeField] private float _boostStrength = 1f, _energyDrainSpeed = 1f, _tiltStrength = 1f, _maxTorque = 0.2f;

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

    private void HandleBoost()
    {
        // Ensure player has energy / infinity energy is enabled
        if (energy > 0 || unlimitedEnergy)
        {
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
}