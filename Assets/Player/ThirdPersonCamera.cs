using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float mouseSensitivity = 3.0f;
    public float minYAngle = -35f;
    public float maxYAngle = 60f;
    public float distance = 7f;
    public float verticalOffset = 1.5f; // Height above the target to look at

    private float currentYaw;
    private float currentPitch;
    private bool initialized = false;

    void LateUpdate()
    {
        if (target == null) return;

        // Initialize yaw and pitch based on current camera position
        if (!initialized)
        {
            Vector3 dir = (transform.position - (target.position + Vector3.up * verticalOffset)).normalized;
            currentPitch = Mathf.Asin(dir.y) * Mathf.Rad2Deg;
            currentYaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            initialized = true;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        currentYaw += mouseX;
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, minYAngle, maxYAngle);

        // Calculate new camera position
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 direction = rotation * Vector3.forward;
        Vector3 desiredPosition = target.position + Vector3.up * verticalOffset - direction * distance;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target.position + Vector3.up * verticalOffset);
    }
}
