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

    #endregion
    [Space]
    #region MOVEMENT
    [Header("            Modifiers")]
    [Space]
    [SerializeField] private float _walkSpeed = 10;
    [SerializeField] private float _jumpForce = 5;


    

    private bool _isLeft;
    private Rigidbody2D _rb;
    #endregion

    private LeverPump _pump;
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
    }

    void Update()
    {
        if (!_isLevering)
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
        else
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
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
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            if (_isLevering)
            {
                _isLevering = false;
                _lever.Switch();
                _cam.GetComponent<CameraZoom>().NewSize(5f);
                return;
            }
            if (!_asCrate)
            {
                RaycastHit2D hit = Raycaster();
                if (hit.collider != null)
                {
                    Debug.Log(hit.transform.name);
                    switch (hit.collider.tag)
                    {
                        case "Pump":
                            if (_pump == null) { _pump = hit.collider.GetComponent<LeverPump>(); }
                            _isLevering = _pump.Switch();
                            _cam.GetComponent<CameraZoom>().NewSize(2.5f);
                            print(_isLevering);
                            break;
                        case "Lever":
                            if (_lever == null){_lever = hit.collider.GetComponent<Lever>();}
                            _isLevering = _lever.Switch();
                            _cam.GetComponent<CameraZoom>().NewSize(7.5f);
                            print(_isLevering);
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
