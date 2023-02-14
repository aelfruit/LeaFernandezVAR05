using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Racer1;
    public GameObject Racer2;

    private Vector3 offset;

    void Start()
    {
        //        offset = transform.position - Player.transform.position;
        offset = transform.position - (Racer1.transform.position + Racer2.transform.position) / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = ((Racer1.transform.position + Racer2.transform.position) / 2) + offset;
    }
}
