using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPump : MonoBehaviour
{
    [SerializeField] [ReadOnly] float _charge;
    [SerializeField] float _speedCharge;

    [SerializeField] float _timeBetweenPump;

    bool _isOn = false;
    bool _isOk = true;

    KeyCode _oldkey = KeyCode.S;
    public bool Switch()
    {
        switch (_isOn)
        {
            case true:
                _isOn = false;
                break;
            case false:
                _isOn = true;
                break;
        }
        print(_isOn);
        return _isOn;
    }

    IEnumerator waitin(float time)
    {
        yield return new WaitForSeconds(time);
        _isOk = true;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && _isOn) 
        {
            if(_oldkey != KeyCode.W && _isOk )
            {
                _isOk = false;
                StartCoroutine(waitin(_timeBetweenPump));
                transform.position -= Vector3.up * .2f;
                _oldkey = KeyCode.W;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && _isOn)
        {
            if(_oldkey != KeyCode.S && _isOk) 
            {
                _isOk = false;
                StartCoroutine(waitin(_timeBetweenPump));
                transform.position += Vector3.up * .2f;
                _oldkey = KeyCode.S;
            }
        }

    }
}
