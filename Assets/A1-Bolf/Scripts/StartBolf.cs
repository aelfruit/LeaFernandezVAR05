using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartBolf : MonoBehaviour
{
    public Button startButton;    
    private void Awake()
    {
        startButton.onClick.AddListener(LoadBolf);
    }

    public void LoadBolf()
    {
        Debug.Log("Time to bring them faux penguins down..");
        SceneManager.LoadScene("BolfScene");
        //SceneManager.LoadScene(nameof(BolfScene));        
    }

}
