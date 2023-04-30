using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public virtual void OnClick()
    {
        if(audioSource != null) audioSource.Play();
    }
}
