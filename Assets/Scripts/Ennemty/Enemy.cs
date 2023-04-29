using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("REF")]
    [Space]
    [SerializeField] GameObject _ship;
    Rigidbody2D _rb;
    TrailRenderer _lr;
    [Space] 
    [Header("Modifiers")]
    [Space]
    [SerializeField] float _speed = 150;
    [CurveRange(0, 0, 1, 1)]
    [SerializeField] AnimationCurve _linethick;


    Vector3 _Dir;
    Vector3 _attackhereBounds;
    Vector3 _attackhereShip;
    bool _goingto;

    //LIFE DASH TRAIL ELSE;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponent<TrailRenderer>();
        _lr.widthCurve = _linethick;



        _attackhereBounds = CreateAttackPoint();
    }

    Vector3 CreateAttackPoint()
    {
        float x = Random.Range(-7.5f,7.5f);
        float y = Random.Range(-.1625f, .1625f);
        print(x + " // " + y);
        _goingto = true;
        Vector3 pos = new(x,y);
        return pos;
    }
        
    // Update is called once per frame
    void Update()
    {
        //print(_ship.transform.position);
        if( _goingto )
        {

            _attackhereShip = _attackhereBounds + _ship.transform.position;
            _Dir = (_attackhereShip - transform.position).normalized;
            _rb.velocity = _speed * Time.deltaTime * _Dir;
        
            if(Mathf.Abs(_rb.velocity.x) > Mathf.Abs(_rb.velocity.y))
            {
                transform.position += new Vector3(0, Mathf.Sin(Time.time * 2f) * .01f);
            }
        

            Debug.DrawLine(transform.position, _attackhereShip, Color.green);
        

            float dist = Vector3.Distance(transform.position,_attackhereShip);
            if(dist <= .8f)
            {
                print("Arrived");
                _goingto = false;
            }

        }
    }

}
