using UnityEngine;

public class Pin : MonoBehaviour
{
    public float rotationUpAxis;
    public bool fellPin;   

    void Start()
    {
        fellPin = false;        
    }

    //Note: deciding whether a pin has fallen or not may be a hard problem! Start with a simple solution first and build out to a more robust one.
    void FixedUpdate()
    {
        rotationUpAxis = Vector3.Angle(Vector3.up, transform.up);

        if (rotationUpAxis > 45)
        {
            fellPin = true;            
        }
    }
}

