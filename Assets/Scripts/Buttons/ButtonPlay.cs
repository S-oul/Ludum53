using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : Button
{
    public override void OnClick()
    {
        base.OnClick();
        SceneManager.LoadScene("Cutscene1");
    }
}
