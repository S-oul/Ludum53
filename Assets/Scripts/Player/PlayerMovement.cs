using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Burst.CompilerServices;
using UnityEditor.SearchService;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform _interactPos;

    #region MOVEMENT
    [SerializeField] private float _walkSpeed = 10;
    [SerializeField] private float _jumpForce = 5;


    private bool _isLeft;
    private Rigidbody2D _rb;
    #endregion

    private bool _asCrate = false;
    private Crate _crate;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            if (!_asCrate)
            {
                RaycastHit2D hit = Raycaster();
                if (hit.collider != null)
                {
                    Debug.Log(hit.transform.name);
                    switch (hit.transform.tag)
                    {
                        case "Lever":
                            hit.transform.GetComponent<Lever>().Switch();
                            break;
                        case "Crate":
                            _crate = hit.transform.GetComponent<Crate>();
                            _crate.interact(_interactPos);
                            _asCrate = true;
                            break;
                    }
                }
                else
                {
                    Debug.Log("problem");
                    
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
            Debug.DrawRay(_interactPos.position, Vector2.right, Color.red);
        }
        else
        {
            hit = Physics2D.Raycast(_interactPos.position, Vector2.left,1);
            Debug.DrawRay(_interactPos.position, Vector2.left, Color.red);
        }
        return hit;
    }

}
