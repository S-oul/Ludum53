using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] PlayerMovement _player;

    [SerializeField] float _angleToRotate = 60;
    float _oldAngle;
    float _allDT = 0;

    public List<Transform> _holes;

    public bool _hole1 = false;
    public bool _hole2 = false;
    public bool _hole3 = false;
    public bool _hole4 = false;
    public bool _hole5 = false;
    public bool _hole6 = false;

    public bool isholed(string str)
    {
        switch (str)
        {
            case "hole1":
                return _hole1;
            case "hole2":
                return _hole2;
            case "hole3":
                return _hole3;
            case "hole4":
                return _hole4;
            case "hole5":
                return _hole5;
            case "hole6":
                return _hole6;
        }
        return false;
    }
    public bool changeholed(string str)
    {
        switch (str)
        {
            case "hole1":
                _hole1 = !_hole1;
                return _hole1;
            case "hole2":
                _hole2 = !_hole2;
                return _hole2;
            case "hole3":
                _hole3 = !_hole3;
                return _hole3;
            case "hole4":
                _hole4 = !_hole4;
                return _hole4;
            case "hole5":
                _hole5 = !_hole5;
                return _hole5;
            case "hole6":
                _hole6 = !_hole6;
                return _hole6;
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsReloading)
        {
            _allDT += Time.deltaTime/_player.ReloadTime;
            float rotate = _oldAngle + Mathf.Lerp(0, _angleToRotate, _allDT);
            transform.eulerAngles = new Vector3(0, 0, rotate);
            //print(rotate);
        }
        else
        {
            _allDT = 0;
            _oldAngle = transform.eulerAngles.z;
        }
        if(Input.GetMouseButtonDown(0))
        {

        }
    }
}
