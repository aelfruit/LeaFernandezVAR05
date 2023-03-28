using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class XRInputController : MonoBehaviour
{
    // from class demo VR Locomotion
    // Publics are usually prefaced with a capital letter.
    public Vector2 Joystick;
    public float RightTrigger;

    private XRInputActions actions;

    // This is called ONLY in the editor when you modify any public
    // fields.
    private void OnValidate()
    {
        // Set the *length* of the joystick vector to never exceed 1.
        Joystick = Vector3.ClampMagnitude(Joystick, 1);
        RightTrigger = Mathf.Clamp01(RightTrigger);
    }

    private void Awake()
    {
        actions = new XRInputActions();

        // If you don't call this, you won't be able to read input.
        // (Why is this not enabled by default? Beats me, ask Unity.)
        actions.Enable();
    }

    private void Update()
    {
        XRHMD hmd = InputSystem.GetDevice<XRHMD>();

        if (hmd != null)
        {
            Joystick = actions.Default.Joystick.ReadValue<Vector2>();
            RightTrigger = actions.Default.RightTrigger.ReadValue<float>();
        }
    }
}

