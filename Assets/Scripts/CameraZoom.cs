using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public bool goUp;
    public bool goDown;

    [SerializeField] private float _newSize;
    [SerializeField] private float _ZoomSpeed = 10;
    void Start()
    {
        
    }

    public void NewSize(float Size)
    {
        _newSize = Size;
        if (_newSize < Camera.main.orthographicSize) { goDown = true; }
        else if (_newSize > Camera.main.orthographicSize) { goUp = true; }

    }

    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, _newSize, Time.deltaTime * _ZoomSpeed);
            if(Mathf.Approximately(Camera.main.orthographicSize, _ZoomSpeed)) { goUp = false; }
        }
        if (goDown)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, _newSize, Time.deltaTime * _ZoomSpeed);
            if (Mathf.Approximately(Camera.main.orthographicSize, _ZoomSpeed)) { goDown = false; }
        }
    }
}
