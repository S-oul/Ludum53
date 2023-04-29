using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InputManager : MonoBehaviour
{
    public enum Maps
    {
        Undifined,

        Up, Down, Left, Right,

        Interact, Fire0, Fire1,

        LShift, LControl, Space
    }
    public struct KeyCodes
    {
        public Maps maps;
        public KeyCode keyCode;
    }
    [BoxGroup("Action Keys")]
    [Header("              Movement")]
    [SerializeField] KeyCode kc_Up = KeyCode.Z;
    [BoxGroup("Action Keys")]
    [SerializeField] KeyCode kc_Down = KeyCode.S;
    [BoxGroup("Action Keys")]
    [SerializeField] KeyCode kc_Left = KeyCode.Q;
    [BoxGroup("Action Keys")]
    [SerializeField] KeyCode kc_Right = KeyCode.D;

    [Header("              Action")]
    [BoxGroup("Action Keys")]
    [SerializeField] KeyCode kc_Interact = KeyCode.F;
    [BoxGroup("Action Keys")]
    [SerializeField] KeyCode kc_Fire0 = KeyCode.Mouse0;
    [BoxGroup("Action Keys")]
    [SerializeField] KeyCode kc_Fire1 = KeyCode.Mouse1;
    [BoxGroup("Action Keys")]

    [Header("             Special")]
    [SerializeField] KeyCode kc_Space = KeyCode.Space;
    [BoxGroup("Action Keys")]
    [SerializeField] KeyCode kc_LShift = KeyCode.LeftShift;
    [BoxGroup("Action Keys")]
    [SerializeField] KeyCode kc_LControl = KeyCode.LeftControl;

    public List<KeyCodes> Inputs;

    void Start()
    {
        Inputs = new();
        KeyCodes UP; UP.maps = Maps.Up; UP.keyCode = kc_Up; Inputs.Add(UP);
        KeyCodes Down; Down.maps = Maps.Down; Down.keyCode = kc_Down; Inputs.Add(Down);
        KeyCodes R; R.maps = Maps.Right; R.keyCode = kc_Right; Inputs.Add(R);
        KeyCodes L; L.maps = Maps.Left; L.keyCode = kc_Left; Inputs.Add(L);

        KeyCodes Inter; Inter.maps = Maps.Interact; Inter.keyCode = kc_Interact; Inputs.Add(Inter);
        KeyCodes F0; F0.maps = Maps.Fire0; F0.keyCode = kc_Fire0; Inputs.Add(F0);
        KeyCodes F1; F1.maps = Maps.Fire1; F1.keyCode = kc_Fire1; Inputs.Add(F1);

        KeyCodes Space; Space.maps = Maps.Space; Space.keyCode = kc_Space; Inputs.Add(Space);
        KeyCodes LShift; LShift.maps = Maps.LShift; LShift.keyCode = kc_LShift; Inputs.Add(LShift);
        KeyCodes LControl; LControl.maps = Maps.LControl; LControl.keyCode = kc_LControl; Inputs.Add(LControl);


    }

    private void OnValidate()
    {
        Start();
    }
}
