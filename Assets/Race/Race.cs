using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Race : MonoBehaviour
{   
    //Two runners will race against one another in a course
    //Runners can be represented as simple shapes (e.g., cubes, spheres)
    //public GameObject[] Racers; use list or array for multi-players
    public GameObject Racer1;   
    public GameObject Racer2;

    public float agileSpeed = 2;    //variable to speed up or slow down in testing

    //Courses consist of tiles of regular terrain, muddy terrain, and a finish line.
    //Runners on regular terrain have a 25% chance to move forward. Otherwise, they do not move this turn.
    //Runners on muddy terrain have a 12.5% chance to move forward. Otherwise, they do not move this turn.

    public GameObject tilePrefab;
    public int courseLength = 5;
    public float tileWidth = 4f;

    //The race will play out in turns.Use coroutines and yield return new WaitForSeconds to create a brief pause between each turn.
    //Every turn, each runner will attempt to move to the next tile in the course.
    public float turnDuration = 1;

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
            }
            else
                tile.GetComponent<TerrainTile>().SetColor(TerrainTile.terrain.muddy);
        }

        StartCoroutine(GameMechanics());
    }

    private IEnumerator GameMechanics()
    {
        int currentTurn = 0;
        while (true)
        {
            Debug.Log($"Turn {currentTurn}");
            yield return new WaitForSeconds(turnDuration);
        }
    }

    public void StartButton()
    {
        GameTexts.text = "Racing Game! Pick and bet on your capsule racer";
        Invoke(nameof(RaceCapsules), 1);
        Invoke(nameof(ClearText), 2);
    }

    public void RaceCapsules()
    {
        //Racer1.transform.position += agileSpeed * Time.deltaTime * new Vector3(10, 0, 0);
        //Racer2.transform.position += agileSpeed * Time.deltaTime * new Vector3(5, 0, 0);
        Debug.Log("Capsules are racing!");
        //

        //Racer2.transform.Translate(Vector3.forward * agileSpeed * Time.deltaTime);

        //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void ClearText()
    {
        GameTexts.text = "";
        Debug.Log("Message banner cleared");
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
       
        //Racer2.transform.Translate(Vector3.right * agileSpeed * Time.deltaTime);

    }
}
