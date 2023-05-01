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

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;


        _shootTime = _timeToShoot;
    }

    void Update()
    {
        if (!_isLevering && !_isPumping)
        {
        #region Movement
            if (Input.GetKey(KeyCode.D))
            {
                _isLeft = false;
                transform.localScale = Vector3.one;
                transform.GetChild(0).transform.localScale = new Vector3(.3f,.3f,.3f);
                _cam.transform.localScale = Vector3.one;
                transform.position += Vector3.right * _walkSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                _isLeft = true;
                transform.localScale = new Vector3(-1,1,1);
                transform.GetChild(0).transform.localScale = new Vector3(-.3f, .3f, .3f);
                transform.position += Vector3.left * _walkSpeed * Time.deltaTime;
                _cam.transform.localScale = new Vector3(-1,1,1);
            }
            if (Input.GetKey(KeyCode.Space) && transform.parent != null) 
            {
                _rb.velocity = Vector2.up * _jumpForce;
            }
        #endregion
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
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            if (_isLevering)
            {
                _isLevering = false;
                _lever.Switch();
                _cam.GetComponent<CameraZoom>().NewSize(5f);
                return;
            }else if (_isPumping)
            {
                _isPumping = false;
                _pump.Switch();
                _cam.GetComponent<CameraZoom>().NewSize(5f);
            }
            else if(!_asCrate)
            {
                RaycastHit2D hit = Raycaster();
                if (hit.collider != null)
                {
                    Debug.Log(hit.transform.name);
                    switch (hit.collider.tag)
                    {
                        case "Pump":
                            if (_pump == null) { _pump = hit.collider.GetComponent<LeverPump>(); }
                            _isPumping = _pump.Switch();
                            _cam.GetComponent<CameraZoom>().NewSize(2.5f);
                            break;
                        case "Lever":
                            if (_lever == null) { _lever = hit.collider.GetComponent<Lever>(); }
                            _isLevering = _lever.Switch();
                            _cam.GetComponent<CameraZoom>().NewSize(7.5f);
                            break;
                    }
                }
            }
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

                }
                else
                {
                    //PLAYSOUND HERE
                }

                _cam.GetComponent<CameraZoom>().NewSize(5f);
                _shootTime = _timeToShoot;

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
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
            hit = Physics2D.Raycast(_interactPos.position, Vector2.right, 1);
        }
        else
        {
            hit = Physics2D.Raycast(_interactPos.position, Vector2.left,1);
        }
        return hit;
    }

}
