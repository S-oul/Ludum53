using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnFade : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float speed;
    private void Start()
    {
        StartCoroutine(UnFadeCo());
    }

    private IEnumerator UnFadeCo()
    {
        while (image.color.a > 0.01f)
        {
            image.color -= new Color(0, 0, 0, Time.deltaTime * speed);
            yield return null;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

    }
}
