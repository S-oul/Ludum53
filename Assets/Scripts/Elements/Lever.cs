using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool _isOn = false;

    //[SerializeField] int Maxangle = 180;
    //[SerializeField] int MinAngle = -67;
    [SerializeField] private float turnSpeed;
    public Collider2D _col;
    public List<GameObject> Lights;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
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
        return _isOn;
    }



    // Update is called once per frame
    void Update()
    {

        if(_isOn)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _animator.SetTrigger("Left");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _animator.SetTrigger("Right");

            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                _animator.SetTrigger("Idle");
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                _animator.SetTrigger("Idle");

            }
            if (Input.GetKey(KeyCode.D))
            {
                foreach (GameObject obj in Lights)
                {
                    float angle = obj.transform.eulerAngles.z;
                    //if (angle > Maxangle+20f) { angle -= 360; }
                    //if (angle < Maxangle)
                    //{
                        obj.transform.eulerAngles += new Vector3(0, 0, 1 * turnSpeed * Time.deltaTime);
                    //    print(angle);

                    //}
                    //else
                    //{
                    //   obj.transform.eulerAngles = new Vector3(0, 0, Maxangle);
                    //}

                }
            }
            if(Input.GetKey(KeyCode.Q)) 
            {
                foreach (GameObject obj in Lights)
                {
                    float angle = obj.transform.eulerAngles.z;
                    //if (angle > Maxangle) { angle -= 360;}
                    //if (angle > MinAngle)
                    //{
                    //    print(angle);
                        obj.transform.eulerAngles += new Vector3(0, 0, -1 * turnSpeed * Time.deltaTime);
                    //}
                    //else
                    //{
                    //    obj.transform.eulerAngles = new Vector3(0, 0, MinAngle);
                    //}

                }
            }
            //FocusLight
            if (Input.GetMouseButtonDown(1))
            {
                foreach (GameObject light in Lights)
                {
                    UVLight uv = light.GetComponent<UVLight>();
                    uv.SetStatus(UVLight.LightStatus.focused);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                foreach (GameObject light in Lights)
                {
                    UVLight uv = light.GetComponent<UVLight>();
                    uv.SetStatus(UVLight.LightStatus.regular);
                }
            }

        }
    }
}
