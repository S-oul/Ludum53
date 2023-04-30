using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPump : MonoBehaviour
{
    [ReadOnly] float _charge;
    [SerializeField] float _speedCharge;

    [SerializeField] float _timeBetweenPump;

    [SerializeField] bool _isOn = false;
    [SerializeField] bool _isOk = false;

    KeyCode _oldkey;
    public bool Switch()
    {
        switch (_isOn)
        {
            case true:
                transform.eulerAngles = new Vector3(0, 0, 30);
                _isOn = false;
                break;
            case false:
                transform.eulerAngles = new Vector3(0, 0, -30);
                _isOn = true;
                break;
        }
        return _isOn;
    }

    IEnumerator waitin(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)) 
        {
            if(_oldkey != KeyCode.W && _isOk)
            {
                //do
                StartCoroutine(waitin(_timeBetweenPump));
                _oldkey = KeyCode.W;
                print("W");
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(_oldkey != KeyCode.Z) 
            {
                //do
                StartCoroutine(waitin(_timeBetweenPump));
                _oldkey = KeyCode.Z;
                print("Z");

            }
        }

    }
}
