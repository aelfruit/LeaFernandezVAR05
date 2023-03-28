using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Composer : MonoBehaviour
{
    public Button removeNoteButton;
    public AudioSource[] notesCollection;
    //public List<AudioSource> composerAudioSources;

    private List<AudioSource> newComposition = new List<AudioSource>();

    //The user should be able to create a musical composition by entering notes one by one.
    //Users should be able to select the note by name, e.g., “A”, “F#”, “Bb” (read as A, F sharp, and B flat).
    //Users should be able to remove a note via the UI.
    //The user should be able to play their composition at any time by pressing a button.

    private void Start()
    {
        removeNoteButton.GetComponent<Button>(); 
        removeNoteButton.onClick.AddListener(RemoveNote);
    }
    public void Compose()
    {
        Debug.Log("Record your composition using numeric keys 1 to 9 and 0 with signs (-) and (=).\nPress the button again or spacebar for playback.");
        //Debug.Log("Press backspace to cancel or remove last note. Bonus function, press v to reverse composition.");

    }
    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            notesCollection[0].Play();
            newComposition.Add(notesCollection[0]);
            Debug.Log("C note pressed");
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            notesCollection[1].Play();
            newComposition.Add(notesCollection[1]);
            Debug.Log("C# or D-flat note pressed");
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            notesCollection[2].Play();
            newComposition.Add(notesCollection[2]);
            Debug.Log("D note pressed");
        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            notesCollection[3].Play();
            newComposition.Add(notesCollection[3]);
            Debug.Log("D# note pressed");
        }

        if (Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            notesCollection[4].Play();
            newComposition.Add(notesCollection[4]);
            Debug.Log("E note pressed");
        }

        if (Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            notesCollection[5].Play();
            newComposition.Add(notesCollection[5]);
            Debug.Log("F note pressed");
        }

        if (Keyboard.current.digit7Key.wasPressedThisFrame)
        {
            notesCollection[6].Play();
            newComposition.Add(notesCollection[6]);
            Debug.Log("F# or G-flat note pressed");
        }

        if (Keyboard.current.digit8Key.wasPressedThisFrame)
        {
            notesCollection[7].Play();
            newComposition.Add(notesCollection[7]);
            Debug.Log("G note pressed");
        }

        if (Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            notesCollection[8].Play();
            newComposition.Add(notesCollection[8]);
            Debug.Log("G# or A-flat note pressed");
        }

        if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            notesCollection[9].Play();
            newComposition.Add(notesCollection[9]);
            Debug.Log("A note pressed");
        }

        if (Keyboard.current.minusKey.wasPressedThisFrame)
        {
            notesCollection[10].Play();
            newComposition.Add(notesCollection[10]);
            Debug.Log("A# or B-flat note pressed");
        }

        if (Keyboard.current.equalsKey.wasPressedThisFrame)
        {
            notesCollection[11].Play();
            newComposition.Add(notesCollection[11]);
            Debug.Log("B note pressed");
        }

        if (Keyboard.current.tabKey.wasPressedThisFrame)    //blank audioSource for restNote
        {
            notesCollection[12].Play();
            newComposition.Add(notesCollection[12]);
            Debug.Log("Rest note added");
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(PlayBackComposition());
            Debug.Log("Composition playback in 2 seconds..");
        }

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {            
            //Debug.Log($"The last note {newComposition[newComposition.Count-1].name} is removed from composition.");
            Invoke(nameof(RemoveNote), 1);  //see if delay is useful otherwise use RemoveNote(); 
        }                

        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            newComposition.Reverse();
            Debug.Log($" The latest composition is reversed. Press spacebar for playback.");
        }
    }

    public void WrapPlayback()  //getting around coroutines for OnClick methods
    {
        StartCoroutine(PlayBackComposition());
        Debug.Log("Composition playback in 2 seconds..");
    }

    private IEnumerator PlayBackComposition()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < newComposition.Count; i++)
        {
            newComposition[i].Play();
            while (newComposition[i].isPlaying)
            {
                yield return null;
            }
            Debug.Log($" The note {newComposition[i].name} is replayed");
        }
    }

    public void RemoveNote()   //moved to method to add delay
    {
        Debug.Log($"The last note {newComposition[newComposition.Count - 1].name} is removed from composition.");
        newComposition.RemoveAt(newComposition.Count - 1);        
    }

}
