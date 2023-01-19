using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuessingGame : MonoBehaviour
{
    public TextMeshProUGUI textGameObject;
    public TMP_InputField inputFieldGameObject;
    public void MyNewFunction()
    {
        Debug.Log("Kamusta!");
        textGameObject.text = ("Hello " + inputFieldGameObject + " how are you doing?");

        //inputFieldGameObject.text = "Name should be here";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
