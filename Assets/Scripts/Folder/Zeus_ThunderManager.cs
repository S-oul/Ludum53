using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zeus_ThunderManager : MonoBehaviour
{
    public float timeBetweenZeus = 10;
    public GameObject ThunderImage;
    private float randomizer = 0;
    private AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        ThunderImage.SetActive(false);
        StartCoroutine(Thundering());
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) { }
    }

    IEnumerator Thundering()
    {
        randomizer = Random.Range(1, 6);
        yield return new WaitForSeconds(timeBetweenZeus);
        if (randomizer == 1)
        {
            ThunderImage.SetActive(true);
            audioData.Play(0);
        }
        yield return new WaitForSeconds(1f);
        Debug.Log(randomizer);
        ThunderImage.SetActive(false);
        StartCoroutine(Thundering());
    }
}
