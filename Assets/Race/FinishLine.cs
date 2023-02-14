using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    //When one runner makes it to the finish line, the race is over and the results are displayed.

    public GameObject[] Racers;
    public Transform finishLine;

    float ShortestDistance = Mathf.Infinity;
    GameObject WinningRacer = null;

    public TextMeshProUGUI gameText;
    
    
    void Start()
    {   
        //Pool the players or racers
        GameObject.FindGameObjectsWithTag("Racer");
        for (int i =0; i < Racers.Length; i++)
        {
            Debug.Log($" {Racers[i].name} will join the game");
        }
    }
        
    void Update()
    {    
        foreach (GameObject Racer in Racers)
        {
            float distance = Vector3.Distance(transform.position, Racer.transform.position);
            if(distance<ShortestDistance)
            {
                ShortestDistance = distance;
                WinningRacer = Racer;
            }            
        }
        // return WinningRacer.name;
        Debug.Log($" {WinningRacer.name} is leading ");                        
    }

    private void FixedUpdate()
    {
        if (WinningRacer.transform.position.x >= finishLine.transform.position.x )
        {
            //anounce winner          
            //Debug.Log($"{WinningRacer.name} made it.");
            gameText.text = $" Whoah {WinningRacer.name} won!";

            //disable movement for players
            GameObject.FindGameObjectsWithTag("Racer");
            for (int i = 0; i < Racers.Length; i++)
            {
                foreach (GameObject Racer in Racers)
                {                    
                    GetComponent<Racer>().enabled = false;                                       
                }
            }
        }
    }
}
