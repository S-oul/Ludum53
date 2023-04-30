using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] DialogueHandler handler;
    private void Start()
    {
        handler.StartDialogue(dialogue);
    }
}
