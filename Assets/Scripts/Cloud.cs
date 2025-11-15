using UnityEngine;

public class Cloud : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Movement")]
    public float moveSpeed;
    public bool isMovingRight = true;
    public Vector3 movementDirection;

    private readonly float _minMoveSpeed = 1, _maxMoveSpeed = 2;
    #endregion

    void Awake()
    {
        // Set random movement speed
        moveSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);

        // Initialize movement direction
        movementDirection = Vector3.right;
    }

    void Update()
    {
        // Move, then check if the cloud has reached the end of the screen
        transform.position += moveSpeed * Time.deltaTime * movementDirection;

        if ((Camera.main.WorldToViewportPoint(_boxCollider.bounds.min).x <= 0 && !isMovingRight) || (Camera.main.WorldToViewportPoint(_boxCollider.bounds.max).x >= 1 && isMovingRight))
        {
            isMovingRight = !isMovingRight;
            movementDirection = -movementDirection;
        }
    }
}