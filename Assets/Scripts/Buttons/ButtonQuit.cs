using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonQuit : Button
{
    public void OnClick()
    {
        base.OnClick();
        Application.Quit();
    }
}
