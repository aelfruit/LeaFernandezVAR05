using UnityEngine;

// from class demo VR Locomotion
public class JoystickTranslate : MonoBehaviour
     
{     
    // Two objects, ourself and the rube goldberg machine.

    // Joystick or grab seem fairly easy from a technical PoV


    public Transform Head;

    public float moveSpeed = 1;

    private XRInputController input;

    private void Awake()
    {
        input = GetComponent<XRInputController>();
    }

    private void Update()
    {
        Vector2 moveInput = input.Joystick;

        // Convert our moveDirection from *local* space to *world* space.
        Vector3 forward = Head.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = Head.right;
        right.y = 0;
        right = right.normalized;

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
} 

