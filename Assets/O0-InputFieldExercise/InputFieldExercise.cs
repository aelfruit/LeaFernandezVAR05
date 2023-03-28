using UnityEngine;
using TMPro;

public class InputFieldExercise : MonoBehaviour
{
    public TextMeshProUGUI textOutput;
    public TMP_InputField inputFieldName;
    public TMP_InputField inputFieldAge;

    public void InputFieldFunction()
    {
        textOutput.text = "Hello " + inputFieldName.text + ",  you are " + inputFieldAge.text + " years old. ";

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
