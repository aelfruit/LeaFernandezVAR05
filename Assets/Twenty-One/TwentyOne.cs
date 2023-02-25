using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TwentyOne : MonoBehaviour
{    
    public GameObject cardPrefab;    
    public TextMeshProUGUI gameText, scoreText;
    public Transform deckOrigin, Dealer, Player;
            
    private bool faceDown;
    
    public float cardOffset = 10f;

    public Button hitButton;
        
    private int scorePlayer, scoreDealer;
    

    public List<CardClass> deckOfCards = new List<CardClass>();
    public List<int> cardIndex = new List<int>();
    
    //List<string> deckOfCards = new List<string>(); //string version for testing

    void Start()
    {
        Debug.Log("Let's play Twenty-One!");
        
        //generate a deck of 52 cards and shuffle        
        GenerateDeck();
        scorePlayer = 0;
        scoreDealer = 0;    

        //dealer gives player 2 cards visible to player
        StartCoroutine(RandomDeal(!faceDown, Player));
        

        //dealer gets 2 cards only one visible to player
        //might be good to position on the dealer side
        StartCoroutine(RandomDeal(faceDown, Dealer));
        


        hitButton.onClick.AddListener(GetNewCard);

        //calculate score or sum of 2 cards with the right value
        //GetScore(0,0,0,0);
    }

    private void Update()
    {
       
    }

    void GenerateDeck()
    {
        //create 52 cards in an accessible List
        Vector3 position = Vector3.zero;        

        for (int i = 0; i< 4; i++)          //4 suits in rows
        {
            position.z += cardOffset + 5f;  //spacing, card height with offset

            for (int h = 0; h < 13; h++)    //13 ranks in columns
            {                
                position.x = h * cardOffset;
                
                GameObject card = Instantiate(cardPrefab,deckOrigin);
                card.transform.position = position;
                
                CardClass deckCard = card.GetComponent<CardClass>() as CardClass;
                deckCard.becomeACard((CardClass.Suits)i, (CardClass.NameRanks)h, h, faceDown); 
                deckOfCards.Add(deckCard);
                
                //string version
                //deckOfCards.Add($"Suit of {i + 1} and Card rank is {h + 1}"); //string version                
            }                                 
        }
        
        //check if deck of cards are generated and make an index for access
        Debug.Log($"Total number of cards {deckOfCards.Count} ");
        for (int i = 0; i < deckOfCards.Count; i++)
        {            
            Debug.Log ($"Card generated is {deckOfCards[i].nameRank} of {deckOfCards[i].suit} \nwith value {deckOfCards[i].value}");
            cardIndex.Add(i);
        }
        gameText.text = "Cards shuffling";
        ShuffleDeck(cardIndex);
        //check if cards are randomized after the function
        for (int i = 0; i < cardIndex.Count; i++)
        {
            Debug.Log($"Card {i} now has value {cardIndex[i]} ");
        }
    }

    public void ShuffleDeck(List<int> cardIndex)
    {    
  
        //swapping 
        int rollDice = cardIndex.Count;
        while (rollDice > 1)
        {
            rollDice--;
            int rand = Random.Range(0, rollDice + 1);
            int temp = cardIndex[rand];
            cardIndex[rand] = cardIndex[rollDice];
            cardIndex[rollDice] = temp;
        }       
        gameText.text = "Cards shuffled";
    }

    private IEnumerator RandomDeal(bool faceDown, Transform Deal)
    {
        yield return new WaitForSeconds(1f);
        int rand = cardIndex.Count - 1;
        int i = cardIndex[rand];
        deckOfCards[i].transform.position = Deal.transform.position;
        //deckOfCards[i].value
        cardIndex.RemoveAt(rand);        
    }

    private IEnumerator Movecard()
    {
        yield return new WaitForSeconds(.5f);

    }

    public void GetNewCard()
    {
        scoreText.text = ($"You're getting a card {scorePlayer}");
        StartCoroutine(RandomDeal(!faceDown, Player));
        //GetCard();
        
    }

    //public int GetCard()
    //{        
    //    int temp = cardIndex[0];
    //    cardIndex.RemoveAt(0);
    //    return temp;
    //}

    ////if there is a stack make a different stack list
    //public void PushCard(int card)
    //{
    //    int i = card;
    //    cardIndex.Add(i);
    //}
    

    void GetScore(int x, int y, int z, int q)
    {
        scorePlayer = x + y;
        scoreDealer = z + q;
        
        scoreText.text = ($"Your score is: {scorePlayer}");


        if (scorePlayer == 21)
        {
            scoreText.text = ($"Blackjack! You got {scorePlayer}");
        }

        if (scorePlayer > scoreDealer)
        {
            scoreText.text = ($" Congratulations, you have the upper hand at {scorePlayer}");
        }

        if (scorePlayer < scoreDealer)
        {
            scoreText.text = ($" Dealer wins with card value of {scoreDealer}");
        }
    }
}

//Game Mechanics
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