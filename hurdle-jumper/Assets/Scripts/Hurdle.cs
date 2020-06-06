using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    float _horizontalSpeed = 8.0f;

    GameObject _myGameManager;

    void Start()
    {
        _myGameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        transform.Translate(
            Vector3.left * Time.deltaTime * _horizontalSpeed, Space.World);
        
        if (transform.position.x <= -12.5)
        {
            // Update the score because the player has dodged this object.
            if(!_myGameManager.GetComponent<GameManager>().GameOver())
            {
                _myGameManager.GetComponent<GameManager>().IncrementScore();
            }
            Destroy(this.gameObject);
        }
    }
}
