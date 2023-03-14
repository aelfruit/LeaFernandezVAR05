using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class Bowling : MonoBehaviour
{
    public Rigidbody ball;
    public Arrow arrow;     //public GameObject arrow;

    public float velocity = 1f;

    public TextMeshProUGUI gameText;
    public TextMeshProUGUI scoreText;
    public Button resetButton;
    

    private int score;
    private Pin[] trianglePins;
    public AudioSource audioSource;

    void Start()
    {
        //In the scene there should be a lane, a bowling ball, and 10 pins set up at the end of the lane.
        ball = ball.GetComponent<Rigidbody>();

        //trianglePins = FindObjectsOfType<Pin>();

        //Along with the message displaying their score, also display a message telling the user they can restart the game by pressing “R”. Allow them to restart if the press R.
        SetScore(0);
        gameText.text = "Press R or the button to Restart the game";

        audioSource = GetComponent<AudioSource>();  //add sound when pin is hit 
    }
    
    void Update()
    {
        //An arrow should automatically rotate back and forth indicating the direction the ball will be bowled.
        //RotateArrow(90);
        arrow.SwingArrow();

        //When the user presses the SPACE key, the ball should be launched in the current direction of the arrow at a specific velocity.
        if (Keyboard.current.spaceKey.isPressed)
        {
            //get direction from arrow
            Vector3 direction = arrow.transform.forward;            
            //ball.AddForce(direction * velocity);  if not using forcemode impulse jack up velocity
            ball.AddForce(direction * velocity, ForceMode.Impulse);
            arrow.CeaseArrow();            
        }

        //If the ball falls off the ledge without hitting any pins, display GUTTER on the screen.
        if (ball.position.y <-1f && score == 0 && ball.position.z >100f || ball.position.z <-100f )
        {
            Debug.Log("Not hitting pins today eh?");
            gameText.text = "GUTTER!!";
        }

        // manual override to check score
        if (Keyboard.current.numpad0Key.wasPressedThisFrame)
            CalculateScore();

        //manual override arrow for testing        
        if (Keyboard.current.leftArrowKey.isPressed)
            RotateArrow(-90);

        if (Keyboard.current.rightArrowKey.isPressed)
            RotateArrow(90);
    }

    private void FixedUpdate()
    {
        CalculateScore();
    }

    private void CalculateScore()
    {        
        score = 0; //if called in update, reset for every count 
        // count all pins and add a point for each that fell
        trianglePins = FindObjectsOfType<Pin>();
        foreach (Pin pin in trianglePins)
        {
            if (pin.fellPin)
            {
                score++;
                SetScore(score);
            }
        }
        
    }

    private void SetScore(int n)
    {
        score = n;
        scoreText.text = "Score:" + score.ToString();

        //If the ball strikes any pins, count the number of pins that fell and display the user’s score on screen. If the user topples all the pins, display STRIKE.
        if (score == 10)
        {
            gameText.text = "STRIKE";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audioSource.Play();
    }

    //manual arrow override
    public void RotateArrow(int y)
    {
        arrow.transform.Rotate(new Vector3(0, y, 0) * Time.deltaTime);
    }
}




