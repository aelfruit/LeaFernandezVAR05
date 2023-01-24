using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuessingGame : MonoBehaviour
{
    //assign this script to a button

    public TextMeshProUGUI textMessageBanner;       //to display mechanics or result 

    public TMP_InputField inputFieldGameObject;     //receive upper limit of range
    public TMP_InputField guessFieldGameObject;     //number to compare to

    int activeGuess;    //translate received numbers to integers  
    int maxNumber;      
    int specialNumber;


    public void GameStart()
    {
        maxNumber = int.Parse(inputFieldGameObject.text);   //Convert.ToInt32       
        specialNumber = Random.Range(1, maxNumber);         //number to compare from
        Debug.Log("Hint! The special number is near " + ++specialNumber);   
    }

    public void CheckGuess()
    {
        activeGuess = int.Parse(guessFieldGameObject.text);

        if (activeGuess == specialNumber)
        {
            textMessageBanner.text = "You're correct! The number was " + specialNumber + " Press the button to play again..";
            
        }
        else if (activeGuess < specialNumber)
        {
            textMessageBanner.text = "Too low, guess again!";
        }
        else if (activeGuess > specialNumber)
        {
            textMessageBanner.text = "Too high, guess again!";
        }
    }
}
