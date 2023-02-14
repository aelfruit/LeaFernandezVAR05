using UnityEngine;


public class Racer : MonoBehaviour
{
    public GameObject Player;
    public float agileSpeed = 1; // see if i need to speed it up

    public float tileWidth = 2f;   // terrain course might change


    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Move()
    {
        Player.transform.position += agileSpeed * new Vector3(tileWidth, 0, 0);
        //Player.transform.position += agileSpeed * Time.deltaTime * new Vector3(tileWidth, 0, 0);
    }
}
