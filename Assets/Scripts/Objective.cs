using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour
{
    [SerializeField] private List<Image> toDisable;
    [SerializeField] private string nextScene;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float loadDelay;

    private SpriteRenderer _fader;

    private void Start()
    {
        _fader = Camera.main.GetComponent<CameraZoom>()._spriteRenderer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Objective hit by " + collision.tag);
        if (collision.CompareTag("Plateforme") || collision.CompareTag("Player")) StartCoroutine(FadeAndLoad());
    }

    private IEnumerator FadeAndLoad()
    {
        foreach (Image image in toDisable) image.enabled = false;
        float tick = 0;
        while (tick < loadDelay) 
        {
            tick += Time.deltaTime;
            _fader.color = new Color(0, 0, 0, Mathf.Clamp(tick, 0, 1));
            yield return null;
        }
        SceneManager.LoadScene(nextScene);
    }
}
