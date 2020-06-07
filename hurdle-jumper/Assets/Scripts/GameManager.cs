using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    private int _myScore;

    // Game characters.
    private Object _myHurdlePrefab;


    // UI elements.
    private GameObject _myScoreText;
    private GameObject _myGameOverText;

    void Start()
    {
        _isGameOver = false;

        // Game over text should not be visible on start.
        _myGameOverText = GameObject.Find("GameOverText");
        _myGameOverText.SetActive(false);

        _myScoreText = GameObject.Find("Score");
        _myHurdlePrefab = Resources.Load("Prefabs/Hurdle");

        StartCoroutine(SpawnHurdle());
    }

    // Adds score by 1 and updates the UI text.
    public void IncrementScore()
    {
        _myScore++;
        _myScoreText.GetComponent<Text>().text = "Score: " + _myScore;
    }

    // Sets the game over state to true.
    public void SetGameOver()
    {
        // Game over.
        _myGameOverText.SetActive(true);
        _isGameOver = true;
    }

    // Returns whether the game is over.
    public bool GameOver()
    {
        return _isGameOver;
    }

    // Spawns a hurdle for the player to jump over.
    IEnumerator SpawnHurdle()
    {
        while (!_isGameOver)
        {
            Instantiate(
                _myHurdlePrefab, 
                new Vector3(12.5f, 0.1f, 0), 
                Quaternion.identity);
            float secondsToWait = Random.Range(0.5f, 3.0f);
            yield return new WaitForSeconds(secondsToWait);
        }
    }

    void Update()
    {
        // If the game is over and the user pressed space we want to reload.
        if(_isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
