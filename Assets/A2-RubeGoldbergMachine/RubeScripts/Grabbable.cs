using UnityEngine;

// from class demo VR Grabbing
public class Grabbable : MonoBehaviour
{
    public Material highlightMaterial;

    // public bool toggleGrabbed;

    private Material defaultMaterial;

    private void Update()
    {
        // SetHighlight(toggleGrabbed);
    }

    private void Awake()
    {
        defaultMaterial = GetComponent<Renderer>().material;
    }

    public void SetHighlight(bool value)
    {
        GetComponent<Renderer>().material
            = value ? highlightMaterial : defaultMaterial;
    }
}

