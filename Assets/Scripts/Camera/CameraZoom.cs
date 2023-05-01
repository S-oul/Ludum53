using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] bool goUp;
    [SerializeField] bool goDown;

    [SerializeField] private float _newSize;
    [SerializeField] private float _ZoomSpeed = 10;

    public SpriteRenderer _spriteRenderer;

    public void Shake(float duration, float force)
    {
        StartCoroutine(shake(duration, force));
    }
    IEnumerator shake(float duration, float force)
    {
        Vector3 startpos = transform.localPosition;

        while (duration > 0)
        {
            float x = Random.Range(-1f, 1f) * force / 10;
            float y = Random.Range(-1f, 1f) * force / 10;

            transform.localPosition += new Vector3(x, y, startpos.z);
            duration -= Time.deltaTime;
            if (duration < 0)
            {
                transform.localPosition = new Vector3(0, 0, startpos.z);
            }
            yield return null;
        }

    }
    public void FadetoBlack(float percent)
    {   
        _spriteRenderer.color = new Color(0,0,0,percent);
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
        _spriteRenderer.transform.localPosition = Vector3.zero + new Vector3(0,0,1);
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
