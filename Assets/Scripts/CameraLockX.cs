using UnityEngine;

public class CameraLockX : MonoBehaviour
{
    // LateUpdate is called once per frame, after Update is finished
    void LateUpdate()
    {
        // Lock X to 0
        transform.position = new(0, transform.position.y, transform.position.z);
    }
}