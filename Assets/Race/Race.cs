using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Race : MonoBehaviour
{

    // tiny runners will race against one another
    // course being represented in the console as ASCII art


    //The race will play out in turns.Use Thread.Sleep to create a brief pause between each turn,
    //and use Console.Clear to remove all text from the console so that an updated image of the course may be redrawn on a clean canvas.
    //Every turn, each runner will attempt to move to the next tile in the course.
    //Runners on regular terrain have a 25% chance to move forward. Otherwise, they do not move this turn.
    //Runners on muddy terrain have a 12.5% chance to move forward. Otherwise, they do not move this turn.
    //(Turns should be of a short enough duration to make this entertaining to watch).
    //When one runner makes it to the finish line, the race is over and the results are displayed

    public GameObject Racer1;
    public GameObject Racer2;
    public float agileSpeed = 50;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        //float lookAngle = 0;
        //transform.position += agileSpeed * Time.deltaTime * moveDirection;

        if (Keyboard.current.dKey.isPressed)
        {
            //Racer1.transform.position = new Vector3(1, 0, 0);
            Racer1.transform.position += agileSpeed * Time.deltaTime * new Vector3(10,0,0);
            moveDirection = new Vector3(1, 0, 0);
            Debug.Log("Racer1 is moving..");
            //lookAngle = 90;            
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {            
            Racer2.transform.position += agileSpeed * Time.deltaTime * new Vector3(10, 0, 0);
            moveDirection = new Vector3(1, 0, 0);
            Debug.Log("Racer2 is catching up..");
        }
    }
}
