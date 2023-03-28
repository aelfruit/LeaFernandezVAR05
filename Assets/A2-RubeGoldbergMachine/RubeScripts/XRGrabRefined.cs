using UnityEngine;

// from class demo VR Grabbing
public class XRGrabRefined : MonoBehaviour
{
    public Transform grabOrigin;
    public float grabRadius = 0.1f;
    public Transform RigTransform;    //to scale and rotate view of machine..
                                      //check position base origin locked at y = 0 "floor"

    public float ScaleMultiplier = 1;

    public bool triggerPressed;

    private Grabbable highlightedObject;
    private Grabbable heldObject;

    private XRInputController input;

    private void Awake()
    {
        input = GetComponent<XRInputController>();
    }

    //how to use oculus grip buttons to scale up

    void Update()
    {
        // Are we holding an object?
        if (heldObject != null)
        {
            if (!triggerPressed)
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;

                heldObject = null;
            }
        }

        // If not, highlight and allow grabbing.
        else
        {
            if (highlightedObject != null)
            {
                highlightedObject.SetHighlight(false);
                highlightedObject = null;
            }
            // Are we hovering over any objects?
            // If so, which one?
            Collider[] cols = Physics.OverlapSphere(grabOrigin.position, grabRadius);

            // Did we hit anything at all?

            foreach (Collider col in cols)
            {
                Grabbable grabbable = col.GetComponent<Grabbable>();

                if (grabbable != null)
                {
                    // Grab the object if the user wants to (i.e., presses the trigger).
                    if (triggerPressed)
                    {
                        heldObject = grabbable;

                        heldObject.transform.parent = transform;
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    else
                    {
                        highlightedObject = grabbable;
                        highlightedObject.SetHighlight(true);
                    }

                    // Exit the loop, we've found something to grab!
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(grabOrigin.position, grabRadius*ScaleMultiplier);
    }
}
