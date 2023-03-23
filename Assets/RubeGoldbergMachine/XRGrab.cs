using UnityEngine;
using UnityEngine.InputSystem.XR;

public class XRGrab : MonoBehaviour
{
    //from grab interaction class demo
    public bool GrabAttempted;
    Rigidbody heldObject;

    XRInputActions xrInputActions;

    bool didDrop;

    //for throwing object
    Vector3 previousPosition;
    Vector3 velocity;           
        
    void Awake()
    {
        xrInputActions = new XRInputActions();
        xrInputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (XRController.rightHand != null)
        {
            GrabAttempted = xrInputActions.Default.Primary.WasPressedThisFrame();
        }

        didDrop = false;

        if (heldObject != null)
        {
            if (GrabAttempted)
            {
                heldObject.transform.parent = null;
                heldObject.isKinematic = false;

                heldObject.velocity = velocity;

                heldObject = null;
                didDrop = true;
                GrabAttempted = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //throwing rigidbody
        if (heldObject != null)
        {
            Vector3 displacement = heldObject.transform.position - previousPosition;

            // transform units per sec
            velocity = displacement / Time.deltaTime;

            previousPosition = heldObject.transform.position;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        //runs after update to avoid re-grabbing dropped heldObject
        if (didDrop == true)
            return;

        if (heldObject == null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                if (GrabAttempted)
                {
                    other.transform.parent = transform;
                    rb.isKinematic = true;

                    heldObject = rb;

                    GrabAttempted = false;
                }
            }
        }
    }
}
