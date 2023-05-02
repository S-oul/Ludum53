using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialCutscene : CutsceneDialogue
{
    [SerializeField] private List<Image> ImagesLine10, ImagesLine11, ImagesLine12, ImagesLine14;

    protected override IEnumerator WaitForDialogueToEnd()
    {
        while (dialogueHandler.IsPlaying) 
        {
            switch (dialogueHandler.CurrentLine)
            {
                case 10: foreach (Image image in ImagesLine10) image.enabled = true; break;
                case 11: foreach (Image image in ImagesLine11) image.enabled = true; break;
                case 12: foreach (Image image in ImagesLine12) image.enabled = true; break;
                case 14: foreach (Image image in ImagesLine14) image.enabled = true; break;
            }

            yield return null; 
        }
        SceneManager.LoadScene(nextScene);
    }
}
