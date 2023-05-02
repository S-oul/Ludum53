using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneStation2 : CutsceneDialogue
{
    [SerializeField] private List<Image> ImagesLine6, ImagesLine8;

    protected override IEnumerator WaitForDialogueToEnd()
    {
        while (dialogueHandler.IsPlaying) 
        {
            switch (dialogueHandler.CurrentLine)
            {
                case 6: foreach (Image image in ImagesLine6) image.enabled = true; break;
                case 8: foreach (Image image in ImagesLine8) image.enabled = true; break;
            }

            yield return null; 
        }
        SceneManager.LoadScene(nextScene);
    }
}
