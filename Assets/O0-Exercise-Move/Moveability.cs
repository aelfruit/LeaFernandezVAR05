using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Moveability : MonoBehaviour
{
    // Create a UI with three input fields for X, Y and Z.
    // Create a button that when pushed will move all of the objects in the scene to that X, Y, Z location.

    public TMP_InputField inputX, inputY, inputZ;
    public Button button;
    public GameObject objectInScene;
    //public List<Transform> objectsInScene;

    //public List<GameObject> gameObjects;

    private int x, y, z;

    public void submitMove()
    {
        x = int.Parse(inputX.text);
        y = int.Parse(inputY.text);
        z = int.Parse(inputZ.text);             
                
        //foreach (Transform t in objectsInScene)
        //{
        //    t.transform.position = new Vector3(x, y, z);
        //}

        Transform XYZ = objectInScene.GetComponent<Transform>();
        objectInScene.transform.position = new Vector3(x, y, z);
        Debug.Log("The objects moved" + x + y + z);
    }


}
