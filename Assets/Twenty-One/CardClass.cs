using UnityEngine;
using TMPro;

public class CardClass : MonoBehaviour
{
    public enum Suits           //standard card suits
    {   Clubs = 0,
        Spades,
        Hearts,
        Diamonds
    }
    public enum NameRanks    //standard card ranks
    
    { Ace, King, Queen, Jack, 
      Ten, Nine, Eight, Seven, 
      Six, Five, Four, Three, Two };
    
    //corresponding value to ranks            
    public int[] Values = 
    { 11, 10, 10, 10, 
      10, 9, 8, 7, 6, 5, 4, 3, 2 };

    public Suits suit;
    public NameRanks nameRank;
    public int value;        
    public TextMeshPro cardText; //show value if I can't generate artwork
    //public bool faceDown;
    
    public void becomeACard(Suits s, NameRanks n, int h, bool faceDown) 
    {
        suit = s;
        nameRank = n;
        value = Values[h];
        //transform.position = pos;
        //transform.rotation = rot; if required add Quaternion rot ^
        if (!faceDown)
        {
            cardText.text = $"{nameRank} of {suit} \nwith value of {value}";
        }
        else
        {
            cardText.text = "";
        }
    }

    public void DestroyCard()
    {
        Destroy(gameObject);
    }
}


/* VERSION USING STRING
public string[] suit =
{
    "Clubs",
    "Spades",
    "Hearts",
    "Diamonds"
};
*/

//public string[] nameRank = 
//{ "Ace", "King", "Queen", "Jack", 
//  "Ten", "Nine", "Eight", "Seven", 
//  "Six", "Five", "Four", "Three", "Two" };

