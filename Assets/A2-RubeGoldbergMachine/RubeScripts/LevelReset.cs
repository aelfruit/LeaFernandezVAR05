using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
    public Transform RiggedPlayer;

    // Call this method to reset the game level

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {        
            // Save the RiggedPlayer's position and rotation
            Vector3 playerPos = RiggedPlayer.position;
            Quaternion playerRot = RiggedPlayer.rotation;


            ResetLevel();

            // Restore the RiggedPlayer's position and rotation
            RiggedPlayer.position = playerPos;
            RiggedPlayer.rotation = playerRot;
        }
    }
    public void ResetLevel()
    {

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
