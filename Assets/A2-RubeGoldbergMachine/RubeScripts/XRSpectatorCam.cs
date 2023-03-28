using UnityEngine;

public class XRSpectatorCam : MonoBehaviour

//from class VR demo, smoothing (normalizing?) camera 
{
    public Transform target;

    public float translateSpeed = 4, rotateSpeed = 4;

    // Using LateUpdate ensures all logic that controls the target
    // headset has executed.
    private void LateUpdate()
    {
        // Lerping (Linear IntERPolation)
        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        // This is not what Lerp is really "intended" for.
        // This is called "exponential easing."
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
        // "Slerp" = "Spherical" Lerp, makes angles do better.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
