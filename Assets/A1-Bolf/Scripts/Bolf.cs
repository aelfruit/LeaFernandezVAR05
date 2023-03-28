using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Bolf : MonoBehaviour
{
    public Rigidbody ball;
    public Arrow arrow;
    public Transform ballOrigin;
    //public Transform freezePoint; optional variable, using trigger makes the rigid bodies sink

    public float velocity = 100f;

    public TextMeshProUGUI gameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI attemptText;
    public Button replayButton;
    public Button menuButton;

    public Scene scene;

    private int score;
    private int scoreTurn1;
    private int scoreTurn2;
    private int bowlAttempt;
    private Pin[] trianglePins;    
    public AudioSource audioSource; //only if there is time         


    void Start()
    {
        //In the scene there should be a lane, a bowling ball, and 10 pins set up at the end of the lane.
        ball = ball.GetComponent<Rigidbody>();        

        SetScore(0);
        gameText.text = "Press spacekey to roll the bolf ball";

        SetBowlAttemptsLeft(2);

        //After the second attempt, display a UI with buttons to Replay or Return to Menu.        
        //canvas.GetComponent<CanvasGroup>().alpha = 1; but this will hide all elements in canvasgroup
        replayButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);       


        //optional SFX if there is time
        audioSource = GetComponent<AudioSource>();  //add sound when pin is hit 
    }

    void Update()
    {
        //An arrow should automatically rotate back and forth indicating the direction the ball will be bowled.        
        arrow.SwingArrow();

        //When the user presses the SPACE key, the ball should be launched in the current direction of the arrow at a specific velocity.
        if (bowlAttempt >=1 )
        {    if (Keyboard.current.spaceKey.wasPressedThisFrame)

            {   //get direction from arrow
                Vector3 direction = arrow.transform.forward;
                //ball.AddForce(direction * velocity);  if not using forcemode impulse then jack up velocity
                ball.AddForce(direction * velocity, ForceMode.Impulse);
                arrow.CeaseArrow();

                //handicap guide
                gameText.text = "Press H to reveal the hidden guide";

                //lose attempt
                bowlAttempt -= 1;
                SetBowlAttemptsLeft(bowlAttempt);                
            }
        }

        if (bowlAttempt == 1)
        {
            scoreTurn1 = score;
            Debug.Log($"First bolf\nhit {scoreTurn1} pinguins");
            gameText.gameObject.SetActive(false);
        }

        if (bowlAttempt == 0)
        {
            scoreTurn2 = score - scoreTurn1;
            Debug.Log($"2nd bolf\nhit {scoreTurn2} pinguins");
            gameText.gameObject.SetActive(true);
            Invoke(nameof(ShowButtons), 5f);
            Invoke(nameof(ShowCourseScore), 6f);
            
        }

        //If the ball falls off the ledge move it back to starting point
        if (ball.position.y < -50f)
        {
            ball.position = ballOrigin.position;
            ball.velocity = Vector3.zero;            
            arrow.ShowArrow();
            Debug.Log($"There are {10 - score} remaining pinguins to topple");
        }

        //If the ball falls off the ledge without hitting any pins, display GUTTER on the screen.
        if (ball.position.y < -50f && score == 0 && ball.position.z > 100f && ball.position.z < -100f)
        {
            Debug.Log("Not hitting pins today eh?");
            gameText.text = "AIR GUTTER!!";
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("BolfCourse1");
        }

        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            arrow.ShowArrow();
            Debug.Log("Bolf handicapped? Hidden guide will reveal itself");
        }

        if (Keyboard.current.nKey.isPressed)
        {
            gameText.text = $"You hit {scoreTurn1} on first turn and {scoreTurn2} on the second turn.\nYour total score is {score}";
        }

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            ball.position = ballOrigin.position;
        }

        // manual override to check score
        if (Keyboard.current.numpad0Key.wasPressedThisFrame)
            CalculateScore();   
        
        //override for main menu
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("BolfMenuScene");
        }

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
        Debug.Log($"Calculated score = {score} fallen pinguins");
    }

    private void SetScore(int n)
    {
        score = n;
        scoreText.text = "Score:" + score.ToString();

        //If the ball strikes any pins, count the number of pins that fell and display the user’s score on screen. If the user topples all the pins, display STRIKE.
        if (score == 10)
        {
            if (scene.name == "BolfCourse2")
            {
                SceneManager.LoadScene("BolfWinScene");
            }

            else
            gameText.text = "STRIKE! All the pretentious pinguins are down!";
            Invoke(nameof(NextCourse), 2f);
        }
    }

    private void SetBowlAttemptsLeft(int m)
    {
        bowlAttempt = m;
        attemptText.text = $"You have {bowlAttempt} bolf(s) remaining";

        if (bowlAttempt == 0)
        attemptText.text = $"You have no bolf attempts left. Press R to retry Course 1. Keep pressing N to checkout your Score Breakdwon.";


    }

    public void NextCourse()
    {
        SceneManager.LoadScene("BolfCourse2");
    }

    public void ShowButtons()
    {
        replayButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }
    public void ShowCourseScore()
    {
        gameText.text = $"You hit {scoreTurn1} on first turn and {scoreTurn2} on the second turn.\nYour total score is {score}";
    }
}




