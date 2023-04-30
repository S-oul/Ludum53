using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("REF")]
    [Space]
    [SerializeField] GameObject _ship;
    [SerializeField] GameObject _player;

    Rigidbody2D _rb;
    TrailRenderer _lr;
    [Space]
    [Header("Modifiers")]
    [Space]
    [SerializeField] float _hp = 1;
    [SerializeField] float _speed = 150;
    [SerializeField] float _crawlingSpeed = 100;
    [SerializeField] float _attackPlayerSpeed = 250;

    public float _speedlightMultiplier;

    [Space]
    [CurveRange(0, 0, 1, 1)]
    [SerializeField] AnimationCurve _linethick;

    


    BoxCollider2D _boxShip;

    Vector3 _Dir;
    Vector3 _attackhereBounds;
    Vector3 _attackhereShip;
    [Space]
    [Header("Debug")]

    [SerializeField] bool _goingto;
    [SerializeField] bool _followpath;
    bool _canGoUp;
    [SerializeField] bool _goUp;
    float _goUpTime = 2;
    [SerializeField] bool _goPlayer;


    //LIFE DASH TRAIL ELSE;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponent<TrailRenderer>();
        _lr.widthCurve = _linethick;

        _boxShip = _ship.transform.GetChild(0).GetComponent<BoxCollider2D>();

        _attackhereBounds = CreateAttackPoint();
    }

    Vector3 CreateAttackPoint()
    {
        float x = Random.Range(-7.5f,7.5f);
        float y = Random.Range(-.1625f, 0) - 3f;
        //print(x + " // " + y);
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
            _rb.velocity = _speed * Time.deltaTime * _speedlightMultiplier * _Dir;
        
            if(Mathf.Abs(_rb.velocity.x) > Mathf.Abs(_rb.velocity.y))
            {
                transform.position += new Vector3(0, Mathf.Sin(Time.time * 2f) * .01f);
            }
        

            Debug.DrawLine(transform.position, _attackhereShip, Color.green);
        

            float dist = Vector3.Distance(transform.position,_attackhereShip);
            if(dist <= .5f)
            {
                print("Arrived");
                transform.parent = _ship.transform;
                _goingto = false;
                _followpath = true;
                _rb.velocity = Vector2.zero;
            }

        }
        if (_followpath)
        {

            float Dist = 0;
            if (transform.localPosition.x <= _boxShip.bounds.center.x)
            {
                _Dir = new Vector3(-_boxShip.bounds.extents.x - transform.position.x, _boxShip.bounds.extents.y - 1 - transform.position.y);
                _rb.velocity = _crawlingSpeed * Time.deltaTime * _Dir.normalized;
                Debug.DrawRay(transform.position, _Dir, Color.yellow);
                Dist = Vector2.Distance(transform.position, new Vector2(-_boxShip.bounds.extents.x, _boxShip.bounds.extents.y - 1));

            }
            if (transform.localPosition.x >= _boxShip.bounds.center.x)
            {
                _Dir = new Vector3(_boxShip.bounds.extents.x - transform.position.x, _boxShip.bounds.extents.y - 1 - transform.position.y).normalized;
                _rb.velocity = _crawlingSpeed * Time.deltaTime * _Dir;
                Debug.DrawRay(transform.position, _Dir, Color.yellow);
                Dist = Vector2.Distance(transform.position, new Vector2(_boxShip.bounds.extents.x, _boxShip.bounds.extents.y - 1));

            }

            //print(Dist);

            if (Dist <= .1f)
            {
                print("Arrived2");
                _canGoUp = true;
                _followpath = false;
                _goUpTime = 1;
            }

        }
        if (_canGoUp)
        {
            _goUpTime -= Time.deltaTime;
            print(_goUpTime);
            if (_goUpTime < 0)
            {
                _canGoUp = false;
                _goUp = true;
                _goUpTime = 1;
            }
        }
        if (_goUp)
        {
            _Dir = Vector2.up;
            _rb.velocity = Time.deltaTime * _speed * _Dir;
            _goUpTime -= Time.deltaTime;
            if( _goUpTime < 0)
            {
                _goUp = false;
                _goUpTime = 2;
                _goPlayer = true;
            }
        }
        if (_goPlayer)
        {
            _Dir = (_player.transform.position - transform.position).normalized;
            _rb.velocity = Time.deltaTime * _attackPlayerSpeed * _Dir;

        }
    }

}
