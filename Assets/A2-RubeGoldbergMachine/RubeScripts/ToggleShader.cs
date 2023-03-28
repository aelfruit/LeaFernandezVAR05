using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleShader : MonoBehaviour
{
    public Shader transparentShader;    //add Custom SeeThroughOutlinesShader
    private Shader opaqueShader;
    private bool isOpaque;

    public string targetTag;            //assign tag to objects that can be transparent

    void Start()
    {
        //transparentShader = Shader.Find("Transparent/Diffuse");
        //useful for changing alpha values
        opaqueShader = Shader.Find("Standard");

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject go in gameObjects)
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.shader = opaqueShader;
            }
        }
        isOpaque = true;
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            ToggleTransparency();
        }
    }

    public void ToggleTransparency()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject go in gameObjects)
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (isOpaque)
                {
                    renderer.material.shader = transparentShader;
                }
                else
                {
                    renderer.material.shader = opaqueShader;
                }
            }
        }
        isOpaque = !isOpaque;
    }
}

//further notes googled out
//https://gamedevtraum.com/en/game-and-app-development-with-unity/unity-tutorials-and-solutions/how-to-make-transparent-material-in-unity-apply-texture-with-transparency/
//https://www.red-gate.com/simple-talk/development/dotnet-development/creating-a-shader-in-unity/

