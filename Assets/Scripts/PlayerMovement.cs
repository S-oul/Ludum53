using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputManager;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputM;
    #region MOVEMENT
    [SerializeField] private float _walkSpeed = 10;
    [SerializeField] private float _runSpeed = 15;
    [SerializeField] private float _sneakSpeed = 5;

    [SerializeField] private bool _isRunning = false;
    [SerializeField] private bool _isSneaking = false;

    private enum DIR
    {
        UP, DOWN, LEFT, RIGHT
    }

    private void DoMove(DIR dir)
    {
        float actualSpeed = _walkSpeed * Time.deltaTime;
        if (_isRunning) { actualSpeed = _runSpeed * Time.deltaTime; }
        if (_isSneaking) { actualSpeed = _sneakSpeed * Time.deltaTime; }

        Vector3 wantedDir = new();
        if (dir == DIR.UP) { wantedDir = Vector3.up; }
        if (dir == DIR.DOWN) { wantedDir = Vector3.down; }
        if (dir == DIR.LEFT) { wantedDir = Vector3.left; }
        if (dir == DIR.RIGHT) { wantedDir = Vector3.right; }

        wantedDir *= actualSpeed;
        transform.position += wantedDir;
    }


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        inputM = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyCodes inputs in inputM.Inputs) 
        {
            if (Input.GetKeyUp(inputs.keyCode))
            {
                //Debug.Log(inputs.keyCode.ToString() + " // " + inputs.maps.ToString() + " Unpressed");

                if (inputs.maps == Maps.LShift)
                {
                    _isSneaking = false;
                    break;
                }
                if (inputs.maps == Maps.LControl)
                {
                    _isRunning = false;
                    break;
                }
            }
            if (Input.GetKey(inputs.keyCode)) 
            {
                Debug.Log(inputs.keyCode.ToString() + " // " + inputs.maps.ToString() + " Pressed");
                switch (inputs.maps)
                {
                    case Maps.Up:
                        DoMove(DIR.UP);
                        break;
                    case Maps.Down:
                        DoMove(DIR.DOWN);
                        break;
                    case Maps.Left:
                        DoMove(DIR.LEFT);
                        break;
                    case Maps.Right:
                        DoMove(DIR.RIGHT);
                        break;
                    case Maps.LShift:
                        _isSneaking = true;
                        break;
                    case Maps.LControl:
                        _isRunning = true;
                        break;
                    case Maps.Fire0:
                        _isRunning = true;
                        break;
                    case Maps.Fire1:
                        _isRunning = true;
                        break;
                }
            }
        }
    }
}
