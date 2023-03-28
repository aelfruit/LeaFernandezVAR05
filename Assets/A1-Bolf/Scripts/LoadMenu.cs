using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public Button loadSceneButton;
    public Scene SceneToLoad;
    void Awake()
    {
        loadSceneButton.onClick.AddListener(LoadMenuScene);
    }

    public void LoadMenuScene()
    {
        //SceneManager.LoadScene(nameof(SceneToLoad));
        SceneManager.LoadScene("BolfMenuScene");
    }

}
