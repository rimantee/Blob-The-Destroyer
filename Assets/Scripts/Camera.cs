using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // The object to follow (your blob)
    public float followSpeed = 5f;   // Speed of following

    private Vector3 offset;

    void Start()
    {
        // Calculate initial offset from the target
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Only follow X and Z, keep camera's Y unchanged
        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            transform.position.y,
            target.position.z + offset.z
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}