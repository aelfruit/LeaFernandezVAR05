using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public Button quitButton;
    private void Awake()
    {
        quitButton.onClick.AddListener(QuitNow);
    }

    public void QuitNow()
    {
        Application.Quit();
    }
}
