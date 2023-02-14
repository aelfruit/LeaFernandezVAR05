using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClass : MonoBehaviour
{
    //public enum suit
    //{   Clubs = 0,
    //    Spades,
    //    Hearts,
    //    Diamonds
    //}
    //{ Diamonds = 3, Hearts = 2, Spades = 1, Clubs = 0 };

    public string[] suit =
    {
        "Clubs",
        "Spades",
        "Hearts",
        "Diamonds"
    };

    public string[] nameRank = 
    { "Ace", "King", "Queen", "Jack", 
      "Ten", "Nine", "Eight", "Seven", 
      "Six", "Five", "Four", "Three", "Two" };
    
    public int[] value = 
    { 11, 10, 10, 10, 
      10, 9, 8, 7, 6, 5, 4, 3, 2 };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
