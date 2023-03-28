using UnityEngine;

public class Arrow1 : MonoBehaviour

{
    public float swingSpeed = 2.0f;     // speed to test or level up challenge
    public float swingAngle = 130.0f;   // angular FOV or field of action? 

    private Quaternion leftRotation;    // rotate towards left
    private Quaternion rightRotation;   // rotate towards right
    private int direction = 1;          // 1 = swinging to the right, -1 = swinging to the left

    void Start()
    {
        leftRotation = transform.rotation;
        rightRotation = Quaternion.Euler(0, -swingAngle, 0 );
    }

    void Update()
    {
        float angle = swingAngle * Mathf.Sin(Time.time * swingSpeed) * direction;
        transform.rotation = Quaternion.Lerp(leftRotation, rightRotation, (angle + swingAngle) / (2 * swingAngle));

        if (Mathf.Approximately(angle, swingAngle) && direction == 1)
        {
            direction = -1;
        }
        else if (Mathf.Approximately(angle, -swingAngle) && direction == -1)
        {
            direction = 1;
        }
    }
}