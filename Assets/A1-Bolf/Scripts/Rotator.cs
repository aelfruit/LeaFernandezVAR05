using UnityEngine;

public class Rotator : MonoBehaviour
{
    //public float rotateX;// = 30f;
    //public float rotateY;// = 90f;
    //public float rotateZ;// = 180f;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
        //transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime);
    }
}
