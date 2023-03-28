using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsSimulation : MonoBehaviour
{
    // VR demo notes
    public bool isPaused;    

    private void Awake()
    {
        Physics.autoSimulation = false;
        isPaused = false;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Pause();
    }

    private void FixedUpdate() 
    {
        if(!isPaused)
        {
            Physics.Simulate(Time.fixedDeltaTime);
        }
    }

    private void Pause() //when not encapsulated separately it does not toggle
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            Debug.Log("Physics Paused");
        }

        else
        {
            Time.timeScale = 1f;
            isPaused = false;
            Debug.Log("Physics Resume");
        }
    }

    //notes
    //private float timer;
    //private void FixedUpdate()
    //{
    //    // Step the physics simulation one frame forward, consuming "delta time" amount of time.
    //    Physics.Simulate(Time.fixedDeltaTime);
    //}

    //if (Physics.autoSimulation)
    //    return; // do nothing if the automatic simulation is enabled

    //timer += Time.deltaTime;

    //// Catch up with the game time.
    //// Advance the physics simulation in portions of Time.fixedDeltaTime
    //// Note that generally, we don't want to pass variable delta to Simulate as that leads to unstable results.
    //while (timer >= Time.fixedDeltaTime)
    //{
    //    timer -= Time.fixedDeltaTime;
    //    Physics.Simulate(Time.fixedDeltaTime);
    //}

}
