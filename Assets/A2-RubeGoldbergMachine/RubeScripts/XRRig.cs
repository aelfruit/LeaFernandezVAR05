using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class XRRig : MonoBehaviour
{
    public Transform Head, Left, Right;
 

    //basic rig from class demo
    private void Update()
    {
        if (XRController.leftHand != null)
        {
            Vector3 leftPosition = XRController.leftHand.devicePosition.ReadValue();
            Quaternion leftRotation = XRController.leftHand.deviceRotation.ReadValue();

            //Left.SetPositionAndRotation(leftPosition, leftRotation);
            Left.localPosition = leftPosition;
            Left.localRotation = leftRotation;
        }

        if (XRController.rightHand !=null)
        {
            Vector3 rightPosition = XRController.rightHand.devicePosition.ReadValue();
            Quaternion rightRotation = XRController.rightHand.deviceRotation.ReadValue();

            //Right.SetPositionAndRotation(rightPosition, rightRotation);
            Right.localPosition = rightPosition;
            Right.localRotation = rightRotation;
        }

        XRHMD hmd = InputSystem.GetDevice<XRHMD>();

        if (hmd != null)
        {
            Vector3 headPosition = hmd.devicePosition.ReadValue();
            Quaternion headRotation = hmd.deviceRotation.ReadValue();

            //Head.SetPositionAndRotation(headPosition, headRotation);
            Head.localPosition = headPosition;
            Head.localRotation = headRotation;
        }
    }
}
