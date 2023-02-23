using UnityEngine;
using UnityEngine.UI;               //buttons
using System.Collections;           //coroutines
using System.Collections.Generic;   //for List
using UnityEngine.InputSystem;      //keyboard controller for testing
//using UnityEngine.SceneManagement;  //load scene
using TMPro;

public class Race : MonoBehaviour
{
    //Two runners will race against one another in a course
    public Button startButton;

    //Runners can be represented as simple shapes (e.g., cubes, spheres)    
    public GameObject[] Racer;    

    public float agileSpeed = 2;    //variable to speed up or slow down 

    //Courses consist of tiles of regular terrain, muddy terrain, and a finish line. || See separate finish Line script.
    //Runners on regular terrain have a 25% chance to move forward. Otherwise, they do not move this turn.
    //Runners on muddy terrain have a 12.5% chance to move forward. Otherwise, they do not move this turn.

    public GameObject tilePrefab;
    private int currentTurn = 0;
    public int courseLength = 10;
    public float tileWidth = 4f;
    private List<TerrainTile> generatedTerrain = new List<TerrainTile>();
    private List<float> chancesToMove = new List<float>();

    //The race will play out in turns.Use coroutines and yield return new WaitForSeconds to create a brief pause between each turn.
    //Every turn, each runner will attempt to move to the next tile in the course.
    public float turnDuration = .2f;

    //When one runner makes it to the finish line, the race is over and the results are displayed.
    public TextMeshProUGUI GameTexts;

    // Start is called before the first frame update
    void Start()
    {        
        //generate terrain

        for (int i = 0; i < courseLength ; i++)
        {
            Vector3 position = Vector3.zero;
            position.x = i * tileWidth;

            GameObject tile = Instantiate(tilePrefab);
            tile.transform.position = position;

            if (Random.value > 0.5f)
            {
                tile.GetComponent<TerrainTile>().SetColor(TerrainTile.terrain.regularLawn);
                //generatedTerrain.Add();
                //chancesToMove.Add(tile.GetComponent<TerrainTile>().ChanceToMove[0]); ??turning this on stops generating tiles
            }
            else
            {
                tile.GetComponent<TerrainTile>().SetColor(TerrainTile.terrain.muddy);                
                //chancesToMove.Add(tile.GetComponent<TerrainTile>().ChanceToMove[1]);
            }
            //Debug.Log($"Chance to move Tile {i} is {chancesToMove[i]}"); //how do i use this for turns?
        }

        startButton.onClick.AddListener(StartButton);
        GameTexts.text = "Racing Game! Pick and bet on your capsule racer";
        
        Invoke(nameof(ClearText), 2);
        StartCoroutine(GameMechanics());
    }

    public void StartButton()
    {
        Debug.Log("Capsules are racing!");
        GameTexts.text = "Racing mode";
        Invoke(nameof(RaceCapsules), 1);
    }


    private IEnumerator GameMechanics()
    {        
        while (true)
        {            
            Debug.Log($"Turn # {currentTurn}");
            RaceCapsules();
            yield return new WaitForSeconds(turnDuration);
            currentTurn++;
        }         
    }


    private void RaceCapsules()
    {
        if (Random.value > 0.5f)
        {
            Racer[0].transform.position += new Vector3(tileWidth, 0, 0);
            GameTexts.text = $"{Racer[0].name} is moving..";
        }

        else
        {
            Racer[1].transform.position += new Vector3(tileWidth, 0, 0);
            GameTexts.text = $"{Racer[1].name} is moving..";
        }
        Invoke(nameof(ClearText), 1);
    }

    void ClearText()
    {
        GameTexts.text = "";
        Debug.Log("Message banner cleared");
    }
       

    void Update()
    {
        if (currentTurn >= courseLength)
        {
            Debug.Log("Game is over");
            StopCoroutine(GameMechanics());
            Invoke(nameof(ClearText), 1);
            GameTexts.text = "Exit";
            Application.Quit();
        }

        //else
        //    StartCoroutine(GameMechanics());


        if (Keyboard.current.dKey.isPressed)
        {
            //transform.position = new Vector3(1, 0, 0);
            Racer[0].transform.position += agileSpeed * Time.deltaTime * new Vector3(tileWidth,0,0);            
            Debug.Log($"{Racer[0].name} is moving..");                    
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {            
            Racer[1].transform.position += agileSpeed * Time.deltaTime * new Vector3(tileWidth, 0, 0);            
            Debug.Log($"{Racer[1].name} is catching up..");
        }  

    }
}

/*
private void RaceCapsulesbackup()
{
    int randomTurn = Random.Range(0, 1);

    //Racer[randomTurn].transform.Translate(Vector3.right * Time.deltaTime);
    Racer[randomTurn].transform.position += agileSpeed * Time.deltaTime * new Vector3(tileWidth, 0, 0);
    GameTexts.text = $"{Racer[randomTurn].name} is moving..";
    Invoke(nameof(ClearText), 1);

}
*/
