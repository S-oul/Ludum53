using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool _isOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Switch()
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
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
