using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
            transform.position += Vector3.right * _walkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            _isLeft = true;
            transform.localScale = new Vector3(-1,1,1);
            transform.position += Vector3.left * _walkSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Space) && transform.parent != null) 
        {
            _rb.velocity = Vector2.up * _jumpForce;
        }

        if (Input.GetKey(KeyCode.F)) 
        {
            switch (_isLeft)
            {
                case false:
                    Physics2D.Raycast(_interactPos.position, Vector2.left);
                    Debug.DrawRay(_interactPos.position, Vector2.left, Color.red);
                    break;
                case true:
                    Physics2D.Raycast(_interactPos.position, Vector2.right);
                    Debug.DrawRay(_interactPos.position, Vector2.right, Color.red);
                    break;
            }

        }



        #region Debug
        if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion
    }
}
