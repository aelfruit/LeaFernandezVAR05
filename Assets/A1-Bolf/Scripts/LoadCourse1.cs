using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadCourse1 : MonoBehaviour
{
    public Button startButton;    
    private void Awake()
    {
        startButton.onClick.AddListener(LoadCourseScene);
    }

    public void LoadCourseScene()
    {
        Debug.Log("Time to bring them faux penguins down in Round 1..");
        SceneManager.LoadScene("BolfCourse1");
                
    }
}
