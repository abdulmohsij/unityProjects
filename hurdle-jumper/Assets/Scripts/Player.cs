using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    float _myHorizontalSpeed = 5.0f;
    float _myJumpForce = 2.0f;
    bool _isGrounded;

    Vector3 _myJumpDirection;
    Rigidbody2D _myRigidBody;
    GameObject _myGameManager;
    Material _myMaterial;

    void Start()
    {
        _myMaterial = GetComponent<Renderer>().material;
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myGameManager = GameObject.Find("GameManager");
        if(_myGameManager == null)
        {
            Debug.Log("NOO GAMEMASDnk");
        }
        _myJumpDirection = new Vector3(0.0f, 3.0f, 0.0f);
        
        StartCoroutine(ChangePlayerColour());
    }

    void OnCollisionEnter2D(Collision2D Col)
    {                                                                          
        if(Col.gameObject.tag == "Floor")
        {
            _isGrounded = true;
        }

        if (Col.gameObject.tag == "Hurdle")
        {
            Debug.Log("Collided");
            _myGameManager.GetComponent<GameManager>().SetGameOver();
            Destroy(this.gameObject);
        }
    }

    // Change the player colour every second.
    IEnumerator ChangePlayerColour()
    {
        while (true)
        {
            _myMaterial.color = 
                new Color(Random.Range(0.5f, 1f), 
                          Random.Range(0.5f, 1f), 
                          Random.Range(0.5f, 1f));
            yield return new WaitForSeconds(1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded)
        {
            _myRigidBody.AddForce(
                _myJumpDirection * _myJumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
        Vector3 translationVector =
            Vector3.right *
            Time.deltaTime *
            Input.GetAxis("Horizontal") *
            _myHorizontalSpeed;

        transform.Translate(translationVector, Space.World);
    }
}
