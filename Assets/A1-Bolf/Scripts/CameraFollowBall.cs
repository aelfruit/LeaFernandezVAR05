using UnityEngine;

public class CameraFollowBall : MonoBehaviour
{
    public GameObject ball;
    private Vector3 offset;

    // stop when certain point is reached to show score and fallen pin.
    private Vector3 lastPosition;
    public Transform freezePoint;    

    void Start()
    {        
        offset = transform.position - ball.transform.position;        
        lastPosition = transform.position;
    }
        
    void LateUpdate()
    {
        transform.position = ball.transform.position + offset;    
        
        if (ball.transform.position.x >= freezePoint.position.x)
        {
            transform.position = lastPosition;
        }
        else
        {
            lastPosition = transform.position;
        }        
    }    
}


