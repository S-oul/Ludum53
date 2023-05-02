using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] float _hp = 200;
    [SerializeField] float _speed = 150;
    [SerializeField] float _crawlingSpeed = 100;
    [SerializeField] float _chasePlayerSpeed = 250;

    [SerializeField] float _timetoKill = 2;
    [SerializeField] float _elapsedTime;

    [SerializeField] Vector2 fadeRandomness;
    public float _speedlightMultiplier;

    [Space]
    [CurveRange(0, 0, 1, 1)]
    [SerializeField] AnimationCurve _linethick;

    SpriteRenderer _spriteRenderer;


    BoxCollider2D _boxShip;

    private bool _facingRight;
    private bool _playerKilled;
    private bool _litUp;
    [SerializeField] private List<UVLight> _currentLights;

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
    public void TakeDamage(float toRemove)
    {
        _hp -= toRemove;
        if( _hp < 0 )
        {


            _spriteRenderer.color = new Color(0, 0, 0, 0);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        /*_lr = GetComponent<TrailRenderer>();
        _lr.widthCurve = _linethick;*/
        if(_ship == null ) { _ship = GameObject.Find("Plateform"); }
        if(_player == null ) { _player = GameObject.Find("Chara"); }
        _spriteRenderer = Camera.main.GetComponent<CameraZoom>()._spriteRenderer;
        _boxShip = _ship.transform.GetChild(0).GetComponent<BoxCollider2D>();

        _attackhereBounds = CreateAttackPoint();
    }

    Vector3 CreateAttackPoint()
    {
        float x = Random.Range(-7.5f,7.5f);
        float y = Random.Range(-.1625f, 0) - 3f;
        //print(x + " // " + y);
        //_goingto = true;
        Vector3 pos = new(x,y);
        return pos;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag + " in Enemy");
        if (collision.CompareTag("lightMask"))
        {
            //print("j'y suis"); 
            _litUp = true;
            _currentLights.Add(collision.transform.parent.GetComponent<UVLight>());
            _speedlightMultiplier = .25f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        print(collision.tag + " out of Enemy");
        if (collision.CompareTag("lightMask"))
        {
            //print("j'y fut");
            _litUp = false;
            _currentLights.Remove(collision.transform.parent.GetComponent<UVLight>());
            _speedlightMultiplier = 1;
        }
    }

    void Update()
    {
        if(_litUp)
        {
            print("lit");
            foreach (UVLight light in _currentLights)
            {
                print("ouille");
                TakeDamage(light.FocusDamage * Time.deltaTime);   
            }
        }
        //print(_ship.transform.position);
        if( _goingto )
        {
            _attackhereShip = _attackhereBounds + _ship.transform.position;
            _Dir = (_attackhereShip - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(_Dir);
            _rb.velocity = _speed * Time.deltaTime * _speedlightMultiplier * _Dir;
        

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
                _rb.velocity = _crawlingSpeed * Time.deltaTime * _speedlightMultiplier * _Dir.normalized;
                Debug.DrawRay(transform.position, _Dir, Color.yellow);
                Dist = Vector2.Distance(transform.position, new Vector2(-_boxShip.bounds.extents.x, _boxShip.bounds.extents.y - 1));

            }
            if (transform.localPosition.x >= _boxShip.bounds.center.x)
            {
                _Dir = new Vector3(_boxShip.bounds.extents.x - transform.position.x, _boxShip.bounds.extents.y - 1 - transform.position.y).normalized;
                _rb.velocity = _crawlingSpeed * Time.deltaTime * _speedlightMultiplier * _Dir;
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
            _rb.velocity = Time.deltaTime * _speed * _speedlightMultiplier * _Dir;
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
            //if(_Dir.x < 0 && !_facingRight) 
            //{ 
            //    _facingRight = true; 
            //    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); 
            //}
            //else if(_Dir.x > 0 && _facingRight)
            //{
            //    _facingRight = false;
            //    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            //}
            _rb.velocity = Time.deltaTime * _chasePlayerSpeed * _Dir;

            float Dist = Vector2.Distance(transform.position, _player.transform.position);
            //print(Dist);
            _spriteRenderer.color = new Color(0, 0, 0, _elapsedTime / _timetoKill);
            if (Dist < 2f)
            {
                _elapsedTime += Time.deltaTime;
                _spriteRenderer.color += new Color(0, 0, 0, Random.Range(fadeRandomness.x, fadeRandomness.y));
                if(_elapsedTime > _timetoKill && !_playerKilled)
                {
                    _playerKilled = true;
                    _spriteRenderer.color = new Color(0,0,0,1);
                    StartCoroutine(_player.GetComponent<PlayerMovement>().PlayerDead());
                }
            }
            else if (!_playerKilled)
            {
                _elapsedTime = Mathf.Clamp(_elapsedTime - Time.deltaTime, 0, 1);
            }

        }

    }
}
