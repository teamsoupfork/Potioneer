using UnityEngine;

/// <summary>
/// Created, referenced and adapted by Chung Ling Kristy 
/// <ref>https://youtu.be/MFQhpwc6cKE</ref>
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 3f;
    public Vector3 offset;
    public bool rotateLook;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        if (rotateLook)
        {
            transform.LookAt(target.transform);
        }
    }
}
