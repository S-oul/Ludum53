using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Rendering;
using UnityEngine.SceneManagement;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Controls controls;
    [SerializeField] private TMPro.TextMeshProUGUI display;
    private bool progress;

    private void Start()
    {
        controls = new Controls();
        controls.UI.ProgressDialogue.performed += context => progress = true;
        controls.UI.ProgressDialogue.Enable();
    }

    public void StartDialogue(Dialogue dialogue, TMPro.TextMeshProUGUI display)
    {
        this.display = display;
        StartDialogue(dialogue);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        display.text = "";
        StartCoroutine(DialogueCo(dialogue));
    }

    private IEnumerator DialogueCo(Dialogue dialogue)
    {
        Queue<string> dialogueQueue = new Queue<string>(dialogue.Lines);
        while (dialogueQueue.Count > 0)
        {
            string line = dialogueQueue.Dequeue();
            foreach (char c in line)
            {
                if (progress) 
                {
                    display.text = line;
                    progress = false;
                    break;
                }
                audioSource.pitch = Random.Range(dialogue.PitchRange.x, dialogue.PitchRange.y);
                audioSource.Play();
                display.text += c;
                yield return new WaitForSeconds(dialogue.CharDelay);
            }
            while (!progress) yield return null;
            progress = false;
            display.text = "";
            yield return new WaitForSeconds(dialogue.LineDelay);
        }
    }

    private void OnDestroy()
    {
        controls.UI.ProgressDialogue.Disable();
    }
}
