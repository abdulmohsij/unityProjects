using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    float _myHorizontalSpeed = 5.0f;
    float _myJumpForce = 2.4f;
    bool _isGrounded;

    [SerializeField]
    private UnityEngine.Object _myExplosionPrefab;

    Vector3 _myJumpDirection;
    Rigidbody2D _myRigidBody;
    GameObject _myGameManager;
    Animator _myAnimator;

    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myGameManager = GameObject.Find("GameManager");
        _myAnimator = GetComponent<Animator>();
        _myJumpDirection = new Vector3(0.0f, 3.0f, 0.0f);
    }

    void OnCollisionEnter2D(Collision2D Col)
    {                                                                          
        if(Col.gameObject.tag == "Floor")
        {
            _isGrounded = true;
            _myAnimator.SetBool("isJumping", false);
        }

        if (Col.gameObject.tag == "Hurdle")
        {
            _myGameManager.GetComponent<GameManager>().SetGameOver();
            _myAnimator.SetBool("isDead", true);
            Instantiate(
                _myExplosionPrefab,
                new Vector2(transform.position.x, transform.position.y),
                Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded)
        {
            _myAnimator.SetBool("isJumping", true);
            _myRigidBody.AddForce(
                _myJumpDirection * _myJumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 newScale= 
            new Vector2(transform.localScale.x, transform.localScale.y);
        if (horizontalInput < 0)
        {
            if (Math.Sign(newScale.x) == -1)
            {
                newScale.x = newScale.x * -1; //Flip X
            }
        }
        else if(horizontalInput > 0)
        {
            if(Math.Sign(newScale.x) == 1)
            {
                newScale.x = newScale.x * -1; //Flip X
            }
        }
        transform.localScale = newScale;

        _myAnimator.SetFloat("myRunningSpeed", Math.Abs(horizontalInput));
        Vector3 translationVector =
            Vector3.right *
            Time.deltaTime *
             horizontalInput *
            _myHorizontalSpeed;

        transform.Translate(translationVector, Space.World);
    }
}
