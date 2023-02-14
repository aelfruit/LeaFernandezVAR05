using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class TwentyOne : MonoBehaviour
{

    //The player will play against the computer(referred to as the dealer going forward).
    //The dealer will give the player two cards.
    //The dealer will then give themselves two cards as well, one which is face down and the other which is face up.
    //Face down cards should not be displayed to the user. Face up cards are displayed to the user.
    //The player can decide to either receive another card or stay.If they decide to stay, they receive no further cards this round.
    //Next, the dealer will take their turn. Each subsequent card they draw should be face up.
    //Once both player and dealer have decided to stay, all cards are revealed, a winner is decided and the round is over.
    //allow the user to decide whether they wish to play another hand or not at the end.
    //Note that as the dealer is a computer player, it will need to be decided whether they stay or not with their current hand.
    //For simplicity, it can just be a random choice(no strategy is required).

    CardClass CardClass;
    public GameObject cardPrefab;

    //public CardClass cardPrefab;

    //private CardClass[] deckOfCards;
    public TextMeshProUGUI deckText;

    public TextMeshProUGUI scoreText;
    private int score;

    List<string> deckOfCards = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Let's play Twenty-One!");
        score = 0;

        //generate a deck of 52 cards        
        GenerateDeck();        

        //dealer gives player 2 cards
        StartCoroutine(RandomDeal());

        //calculate score
        GetScore();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDeck() //string version; how do i update and access the card class?
    {
        //create 52 cards
        
        //List<string> deckOfCards = new List<string>();
        for (int i = 0; i< 4; i++) //suits
        {
            //Debug.Log($"Suit of {i+1}");
            
            for (int h = 0; h < 13; h++) // ranks
            {
                Debug.Log($"Suit of {i+1} and Card rank is {h+1}" );                              
                deckOfCards.Add($"Suit of {i + 1} and Card rank is {h + 1}");
            }
        }

        for (int i = 0; i < deckOfCards.Count; i++)
        {
            Debug.Log($"{deckOfCards[i]} ");
            deckText.text = $" {deckOfCards[i]} ";
        }
    }

    private IEnumerator RandomDeal()
    {
        int randomCard;

        //Debug.Log("Two cards to be revealed.");
        
        randomCard = (Random.Range(1,52));
        yield return randomCard;
        

        GameObject card = Instantiate(cardPrefab);

        //access and subtract chosen card from deck;
    }

    void GetScore()
    {
        score++;
        scoreText.text = ($"Your score is: {score}");
    }



}
