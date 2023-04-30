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

    float _shootTime = 0;

    


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
                transform.position += Vector3.right * _walkSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                _isLeft = true;
                transform.localScale = new Vector3(-1,1,1);
                transform.GetChild(0).transform.localScale = new Vector3(-.3f, .3f, .3f);
                transform.position += Vector3.left * _walkSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.Space) && transform.parent != null) 
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

        if (Input.GetMouseButton(0))
        {
            Debug.DrawRay(transform.position, (_visorpos.position - transform.position ).normalized* _shootDist, Color.magenta);

            _shootTime -= 1;
            if(_shootTime < 1)
            {
                Shoot();
                _shootTime = _timeToShoot;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
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
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, (_visorpos.position - transform.position ).normalized, _shootDist, 8);
        if (hit.collider == null)
        {

            print("caca");
        }
        else
        {
            print(hit.collider.gameObject.name);
        }
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
