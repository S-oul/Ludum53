using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float _posPercent = 0f;
    [SerializeField] float _speed = 2;
    [SerializeField] Transform _start;
    [SerializeField] Transform _oldPos;
    [SerializeField] Transform _end;

    Rigidbody2D _rb;
    LeverPump _pump;
    HingeJoint2D _joint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.transform.CompareTag("Enemy"))
        collision.transform.parent = transform;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Enemy") && collision.transform.parent != transform)
            collision.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }

    void Start()
    {
        _joint = GetComponent<HingeJoint2D>();
        _pump = GetComponentInChildren<LeverPump>();
        _rb = GetComponent<Rigidbody2D>();  

        transform.position = _start.position;
        _oldPos = transform;
        _joint.connectedAnchor = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (_pump.Charge > 0)
        {
            _posPercent += Time.deltaTime / 100 * _speed;
        }

        transform.position = Vector3.Lerp(_start.position, _end.position, _posPercent);
        


        _joint.connectedAnchor = new Vector2(transform.position.x, transform.position.y);
    }
}
