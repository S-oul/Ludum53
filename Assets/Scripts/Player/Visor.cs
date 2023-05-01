using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visor : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        transform.eulerAngles += new Vector3(0, 0, 1) * _rotateSpeed * Time.deltaTime;
       /* Vector3 size = new Vector3(.3f, .3f, 1);
        transform.localScale = new Vector3(Mathf.Sin(Time.time) * size.x/3+ .15f, Mathf.Sin(Time.time) * size.y /3 + .15f);*/

    }
}
