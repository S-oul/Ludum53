using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zeus_ThunderManager : MonoBehaviour
{
    [SerializeField] private Vector2 timeBetweenZeus;
    [SerializeField] private float thunderDuration;
    [SerializeField] private Vector2 audioPitchRange;
    [SerializeField] private GameObject ThunderImage;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        ThunderImage.SetActive(false);
        StartCoroutine(Thundering());
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator Thundering()
    {
        yield return new WaitForSeconds(Random.Range(timeBetweenZeus.x, timeBetweenZeus.y));
        ThunderImage.SetActive(true);
        audioSource.Stop();
        audioSource.pitch = Random.Range(audioPitchRange.x, audioPitchRange.y);
        audioSource.Play(0);
        yield return new WaitForSeconds(thunderDuration);
        ThunderImage.SetActive(false);
        StartCoroutine(Thundering());
    }
}
