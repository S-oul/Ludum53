using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private bool _istake = false;
    private Transform _target;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void interact(Transform obj)
    {
        _istake = true; 
        _target = obj;
        print(_istake);
    }
    public void stopInteract()
    {
        _istake = false;
        _target = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (_istake)
        {
            transform.position = _target.position;
        }
    }
}
