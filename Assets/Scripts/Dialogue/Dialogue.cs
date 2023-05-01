using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private float lineDelay, charDelay;
        public float LineDelay => lineDelay;
        public float CharDelay => charDelay;
    [SerializeField] private Vector2 pitchRange;
        public Vector2 PitchRange => pitchRange;

    [SerializeField] private List<string> lines;
        public List<string> Lines => lines;

    [SerializeField] private Sprite portrait;
        public Sprite Portrait => portrait; 
}