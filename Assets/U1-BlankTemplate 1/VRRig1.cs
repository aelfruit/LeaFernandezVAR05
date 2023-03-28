using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.InputSystem.XR;

public class VRRig1 : MonoBehaviour
{
    public Transform head, left, right;

    private void Awake()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();

        Debug.Log(XRGeneralSettings.Instance.Manager.activeLoader);
    }

    private void Update()
    {
        if (XRController.leftHand != null)
        {
            // Update the transforms of the components of our VR rig,
            // i.e., the head and hands
            // "i.e." just means "in other words"

            Vector3 leftPosition = XRController.leftHand.devicePosition.ReadValue();
            Quaternion leftRotation = XRController.leftHand.deviceRotation.ReadValue();

            left.SetPositionAndRotation(leftPosition, leftRotation);
        }

        if (XRController.rightHand != null)
        {
            Vector3 rightPosition = XRController.rightHand.devicePosition.ReadValue();
            Quaternion rightRotation = XRController.rightHand.deviceRotation.ReadValue();

            right.SetPositionAndRotation(rightPosition, rightRotation);
        }

        //if ( != null)
        //{
        //    Vector3 rightPosition = XRController.rightHand.devicePosition.ReadValue();
        //    Quaternion rightRotation = XRController.rightHand.deviceRotation.ReadValue();

        //    right.SetPositionAndRotation(rightPosition, rightRotation);
        //}
    }
}