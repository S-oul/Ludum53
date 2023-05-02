using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverPump : MonoBehaviour
{
    [SerializeField] [ReadOnly] float _charge;
    [SerializeField] float _depletionFactor = 2f;
    [SerializeField] float _addToCharge;


    [SerializeField] float _timeBetweenPump;

    [SerializeField] private Image _batteryFill;
    public Animator _animator;

    bool _isOn = false;
    bool _isOk = true;

    KeyCode _oldkey = KeyCode.S;

    public float Charge
    {
        get => _charge;
    }

    public bool Switch()
    {
        switch (_isOn)
        {
            case true:
                _isOn = false;
                _animator.SetBool("Ispumping", false);

                break;
            case false:
                _isOn = true;
                _animator.SetBool("Ispumping", true);
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
        _animator = transform.GetComponentInChildren<Animator>();
    }
    void AddCharge(float toAdd)
    {
        _charge += toAdd;
        if(_charge < 0) { _charge = 0; }
        if(_charge > 1) { _charge = 1; }

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
                _animator.SetBool("Ispumping", true);

                //transform.position -= Vector3.up * .2f;
                _oldkey = KeyCode.W;

                AddCharge(_addToCharge);
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && _isOn)
        {
            if(_oldkey != KeyCode.S && _isOk) 
            {
                _isOk = false;
                StartCoroutine(waitin(_timeBetweenPump));
                _animator.SetBool("Ispumping", false);

                //transform.position += Vector3.up * .2f;
                _oldkey = KeyCode.S;
                
                AddCharge(_addToCharge);
            }
        }
        AddCharge(-Time.deltaTime/100 * _depletionFactor);
        _batteryFill.fillAmount = _charge;
    }
}
