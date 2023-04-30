using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    private float length, startPos;
    [SerializeField] private GameObject camera;
    [SerializeField] private float parralaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float offsetFromCamera = (camera.transform.position.x * (1 - parralaxEffect));
        float offsetFromStart = (camera.transform.position.x * parralaxEffect);

        transform.position = new Vector3(startPos + offsetFromStart, transform.position.y, transform.position.z);
        if (offsetFromCamera > startPos + length) startPos += length;
        else if(offsetFromCamera < startPos - length) startPos -= length;
    }
}
