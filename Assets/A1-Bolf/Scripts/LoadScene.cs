using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
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
        SceneManager.LoadScene("BolfLoadScene");
    }

}
