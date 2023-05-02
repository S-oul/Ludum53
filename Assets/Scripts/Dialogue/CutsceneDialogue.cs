using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneDialogue : MonoBehaviour
{
    [SerializeField] protected DialogueHandler dialogueHandler;
    [SerializeField] protected Dialogue dialogue;
    [SerializeField] protected string nextScene;
    [SerializeField] protected Animator fade;

    private void Start()
    {
        dialogueHandler.StartDialogue(dialogue);
        StartCoroutine(WaitForDialogueToEnd());
    }

    protected virtual IEnumerator WaitForDialogueToEnd()
    {
        while(dialogueHandler.IsPlaying) { yield return null; }
        SceneManager.LoadScene(nextScene);
    }
}
