using Unity.VisualScripting;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Movement")]
    public float moveSpeed;
    [SerializeField] private Vector2 _possibleMovementSpeeds = new(1, 2);
    public bool isMovingRight = true;
    public Vector3 movementDirection;

    #endregion

    void Awake()
    {
        // Set random movement speed
        moveSpeed = Random.Range(_possibleMovementSpeeds.x, _possibleMovementSpeeds.y);

        // Pick random movement direction (50/50 left/right)
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            movementDirection = Vector3.left;
            isMovingRight = false;
        }
        if (rand == 1)
        {
            movementDirection = Vector3.right;
            isMovingRight = true;
        }
    }

    void OnDestroy()
    {
        transform.DetachChildren();
    }

    void Update()
    {
        MoveCloud();
    }

    protected void MoveCloud()
    {
        // Move, then check if the cloud has reached the end of the screen
        transform.position += moveSpeed * Time.deltaTime * movementDirection;

        if ((Camera.main.WorldToViewportPoint(_boxCollider.bounds.min).x <= 0 && !isMovingRight) || (Camera.main.WorldToViewportPoint(_boxCollider.bounds.max).x >= 1 && isMovingRight))
        {
            isMovingRight = !isMovingRight;
            movementDirection = -movementDirection;
        }
    }
    public void SpawnBattery(GameObject batteryPrefab)
    {
        // Spawn battery as a child object 0.5 units above the current position
        Instantiate(batteryPrefab, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, transform);
    }
}