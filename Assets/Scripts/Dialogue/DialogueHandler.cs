using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Rendering;
using UnityEngine.SceneManagement;

public class DialogueHandler : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private TMPro.TextMeshProUGUI display;
    [SerializeField] private Image portraitFrame;
    [SerializeField] private Animator dialogueProgressSymbolAnimator;
    [SerializeField] private float charSoundDelay;
    [SerializeField] private bool forceUpperCase;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> charSounds;

    private Controls controls;
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
        if(dialogue.Portrait != null) portraitFrame.sprite = dialogue.Portrait;
        StartCoroutine(DialogueCo(dialogue));
    }

    private IEnumerator DialogueCo(Dialogue dialogue)
    {
        Queue<string> dialogueQueue = new Queue<string>(dialogue.Lines);
        while (dialogueQueue.Count > 0)
        {
            Coroutine soundCo = StartCoroutine(SoundCo(dialogue.PitchRange));
            string line = forceUpperCase ? dialogueQueue.Dequeue().ToUpper() : dialogueQueue.Dequeue();
            foreach (char c in line)
            {
                if (progress) 
                {
                    display.text = line;
                    progress = false;
                    break;
                }
                display.text += c;
                yield return new WaitForSeconds(dialogue.CharDelay);
            }
            StopCoroutine(soundCo);
            dialogueProgressSymbolAnimator.SetTrigger("FadeIn");
            while (!progress) yield return null;
            dialogueProgressSymbolAnimator.SetTrigger("FadeOut");
            progress = false;
            display.text = "";
            yield return new WaitForSeconds(dialogue.LineDelay);
        }
    }

    private IEnumerator SoundCo(Vector2 pitchRange)
    {
        while(true) 
        {
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.PlayOneShot(charSounds[Random.Range(0, charSounds.Count)]);
            yield return new WaitForSeconds(charSoundDelay);
        }
    }

    private void OnDestroy()
    {
        controls.UI.ProgressDialogue.Disable();
    }
}
