using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool _isOn = false;

    [SerializeField] int Maxangle = 180;
    [SerializeField] int MinAngle = -67;
    public Collider2D _col;
    public List<GameObject> Lights;

    // Start is called before the first frame update
    void Start()
    {
        
    }
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



    // Update is called once per frame
    void Update()
    {

        if(_isOn)
        {
            if (Input.GetKey(KeyCode.D))
            {
                foreach (GameObject obj in Lights)
                {
                    float angle = obj.transform.eulerAngles.z;
                    if (angle > Maxangle+20f) { angle -= 360; }
                    if (angle < Maxangle)
                    {
                        obj.transform.eulerAngles += new Vector3(0, 0, 1);
                        print(angle);

                    }
                    else
                    {
                        obj.transform.eulerAngles = new Vector3(0, 0, Maxangle);
                    }

                }
            }
            if(Input.GetKey(KeyCode.Q)) 
            {
                foreach (GameObject obj in Lights)
                {
                    float angle = obj.transform.eulerAngles.z;
                    if (angle > Maxangle) { angle -= 360;}
                    if (angle > MinAngle)
                    {
                        print(angle);
                        obj.transform.eulerAngles += new Vector3(0, 0, -1);
                    }
                    else
                    {
                        obj.transform.eulerAngles = new Vector3(0, 0, MinAngle);
                    }

                }
            }

        }
    }
}
