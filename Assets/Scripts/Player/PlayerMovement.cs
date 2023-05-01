using JetBrains.Annotations;
using NaughtyAttributes;
using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour
{
    #region REF
    [Header("              Ref")]
    [Space]

    [SerializeField] Camera _cam; 
    [SerializeField] Transform _interactPos;
    [SerializeField] Transform _visorpos;
    [SerializeField] Barrel _barrel;
    [SerializeField] BulletsManager _bulletsManager;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _animation;
    public Animator _animator;

    #endregion
    [Space]
    #region MOVEMENT
    [Header("            Modifiers")]
    [Space]
    [SerializeField] private float _walkSpeed = 10;
    [SerializeField] private float _jumpForce = 5;
    [Tooltip("En FRAME")]
    [SerializeField] float _timeToShoot = 15;
    [SerializeField] float _shootDist = 2;
    [SerializeField] float _reloadTime = 4;
    [SerializeField] int _bulletCount = 0;
    [SerializeField] int _bulletInInvetory = 3;
    public bool CanShoot = false;
    float _shootTime = 0;



    private bool _isReloading = false;
    private bool _isLeft;
    private Rigidbody2D _rb;
    #endregion

    private LeverPump _pump;
    private bool _isPumping;

    private Lever _lever;
    private bool _isLevering;

    private bool _asCrate = false;
    private Crate _crate;


    [Space(100)]
    [Header("Debug")]
    public bool _debug;

    [HideInInspector]public bool IsReloading { get => _isReloading;}
    public float ReloadTime { get => _reloadTime;}
    public int BulletCount { get => _bulletCount; set => _bulletCount = value; }
    public int BulletInInvetory { get => _bulletInInvetory; set => _bulletInInvetory = value; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
        _animator = _animation.GetComponent<Animator>();
        _shootTime = _timeToShoot;

        for(int i = _bulletInInvetory -1; i>= 0; i--)
        {
            GameObject go = Instantiate(_bulletPrefab);
            go.transform.parent = Camera.main.transform;
            go.transform.localPosition = new Vector3(7 - (i * .7f), -4.10f, 1);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            StartCoroutine(PlayerDead());
        }
    }
    void Update()
    {
        if (!_isLevering)
        {
            if (!_isPumping)
            {
                #region Movement
                if (Input.GetKey(KeyCode.D))
                {
                    _isLeft = false;
                    _animator.SetBool("IsWalking", true);
                    _animation.localScale = new Vector3(-1, 1, 1);
                    /*transform.localScale = Vector3.one;
                    transform.GetChild(0).transform.localScale = new Vector3(.3f, .3f, .3f);
                    _cam.transform.localScale = Vector3.one;*/
                    transform.position += Vector3.right * _walkSpeed * Time.deltaTime;
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    _isLeft = true;
                    _animator.SetBool("IsWalking", true);
                    _animation.localScale = new Vector3(1, 1, 1);
                    /* 
                      transform.localScale = new Vector3(-1, 1, 1);
                    transform.GetChild(0).transform.localScale = new Vector3(-.3f, .3f, .3f);
                    _cam.transform.localScale = new Vector3(-1, 1, 1);*/
                    transform.position += Vector3.left * _walkSpeed * Time.deltaTime;
                }
                else
                {
                    _animator.SetBool("IsWalking", false);
                }
                if (Input.GetKey(KeyCode.Space) && transform.parent != null)
                {
                    _rb.velocity = Vector2.up * _jumpForce;
                }
                #endregion
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!_asCrate)
            {
                RaycastHit2D hit = Raycaster();
                if (hit.collider != null)
                {
                    Debug.Log(hit.transform.name);
                    switch (hit.collider.tag)
                    {
                        case "Crate":
                            _crate = hit.collider.GetComponent<Crate>();
                            _crate.interact(_interactPos);
                            _asCrate = true;
                            break;
                    }
                }
            }
            else
            {
                _crate.stopInteract();
                _asCrate = false;
                _crate = null;
            }
            
        }
        if (_isLevering)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _animator.SetTrigger("LeverPull");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _animator.SetTrigger("LeverPull");

            }
        }
        if(_isPumping)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _animator.SetTrigger("PumpDown");
            }
            if(Input.GetKeyDown(KeyCode.W)) 
            {
                _animator.SetTrigger("PumpUp");

            }
        }
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            if (_isLevering)
            {
                _isLevering = false;
                _animator.SetTrigger("LeverExit");
                _lever.Switch();
                _cam.GetComponent<CameraZoom>().NewSize(5f);
                return;
            }else if (_isPumping)
            {
                _isPumping = false;
                _pump.Switch();
                _animator.SetTrigger("PumpExit");

                _cam.GetComponent<CameraZoom>().NewSize(5f);
            }
            else if(!_asCrate)
            {
                RaycastHit2D hit = Raycaster();
                if (hit.collider != null)
                {
                    switch (hit.collider.tag)
                    {
                        case "Pump":
                            Debug.Log(hit.collider.name);
                            if (_pump == null) { _pump = hit.collider.GetComponent<LeverPump>(); }
                            _animator.SetTrigger("PumpEnter");
                            _isPumping = _pump.Switch();
                            _cam.GetComponent<CameraZoom>().NewSize(2.5f);
                            break;
                        case "Lever":
                            if (_lever == null) { _lever = hit.collider.GetComponent<Lever>(); }
                            _animator.SetTrigger("LeverEnter");
                            _isLevering = _lever.Switch();
                            _cam.GetComponent<CameraZoom>().NewSize(7.5f);
                            break;
                    }
                }
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            _animator.SetBool("GunEnter", true);
        }
        if (Input.GetMouseButton(0) && _isReloading == false && CanShoot)
        {
            //Debug.DrawRay(transform.position, (_visorpos.position - _interactPos.position).normalized * _shootDist, Color.magenta);
            _shootTime -= 1;
            _cam.GetComponent<CameraZoom>().NewSize(_cam.orthographicSize - .015f);

            if (_shootTime == 0)
            {
                if(_bulletCount > 0)
                {
                    Shoot();
                    print(_bulletCount);
                    //PLAYSOUND HERE
                    _bulletCount--;
                    string str;
                    foreach(Transform t in _barrel._holes)
                    {
                        //print(t.name);
                        if (_barrel.isholed(t.name))
                        {
                            //print("OUI");
                            _barrel.changeholed(t.name);
                            t.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
                            break;
                        }
                    }
                }
                else
                {
                    _isReloading = true;
                    StartCoroutine(Reload(_reloadTime/2));
                    //PLAYSOUND HERE
                }

                _cam.GetComponent<CameraZoom>().NewSize(5f);
                _shootTime = _timeToShoot;

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _animator.SetBool("GunExit", true);
            _cam.GetComponent<CameraZoom>().NewSize(5f);
            _shootTime = _timeToShoot;
        }

        #region Debug
        if (_debug)
        {
            if (!_isLeft)
            {
                Debug.DrawRay(_interactPos.position, Vector2.right, Color.red);
            }
            else
            {
                Debug.DrawRay(_interactPos.position, Vector2.left, Color.red);
            }

        }

        if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion
    }

    void Shoot()
    {
        _isReloading = true;
        StartCoroutine(Reload(_reloadTime));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (_visorpos.position - _interactPos.position).normalized,_shootDist);
        if(hit.collider != null)
        {

            print(hit.collider.name);   
            if (hit.transform.CompareTag("Enemy"))
            {
                print("HIT !");
                //_cam.GetComponent<CameraZoom>().Shake(.1f, 1f);
                hit.transform.GetComponent<Enemy>().TakeDamage(999);
            }
        }
       
    }
    public IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        _isReloading = false;
    }

    private RaycastHit2D Raycaster()
    {
        RaycastHit2D hit;
        if (!_isLeft)
        {
            hit = Physics2D.CapsuleCast(_interactPos.position, Vector2.one, new CapsuleDirection2D(),0, Vector2.right, 3);
        }
        else
        {
            hit = Physics2D.CapsuleCast(_interactPos.position, Vector2.one, new CapsuleDirection2D(), 0, Vector2.left, 3);

        }
        return hit;
    }

}
