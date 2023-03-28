using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float swingSpeed = 2f;       //speed to test or level up challenge
    public float swingAngle = 180f;     //angular FOV or field of action?

    private Quaternion leftRotation;    //rotate towards or turn left;
    private Quaternion rightRotation;
    private int direction = 1;          //-1 swing towards left

    private void Start()
    {
        leftRotation = transform.rotation;
        rightRotation = Quaternion.Euler(0, -swingAngle, 0);
    }

    //An arrow should automatically rotate back and forth indicating the direction the ball will be bowled
    public void SwingArrow()
    {
        float angle = swingAngle * Mathf.Sin(Time.time * swingSpeed) * direction;
        transform.rotation = Quaternion.Lerp(leftRotation, rightRotation, (angle + swingAngle) / (2 * swingAngle));

        if (Mathf.Approximately(angle, swingAngle) && direction == 1)
            direction = -1;
        else if (Mathf.Approximately(angle, -swingAngle) && direction == -1)
            direction = 1;
    }

    public void CeaseArrow()
    {        
        
        gameObject.SetActive(false);
        //can add a particle effect for fun if there is time
    }

    public void ShowArrow()
    {
        gameObject.SetActive(true);
    }

}
