using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public bool gameOver;
    public bool gameStart;
    public CanvasGroup GameOverCanvas;
    public CanvasGroup StartGameCanvas;
    public int moneyStat;

    // Use this for initialization
    void Start()
    {
        gameStart = true;
        gameOver = false;
        GameOverCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown("b"))
        {
            gameOver = true;
        }
        if (gameOver == true)
        {
            Debug.Log("UI Recognises Game Over");
            GameOverCanvas.gameObject.SetActive(true);
        }
        if (gameStart == true)
        {
            Debug.Log("UI calls game start.");
            StartGameCanvas.gameObject.SetActive(true);
        }
        checkForInput();
    }
    public void checkForInput()
    {
        //start game
        if (gameStart == true && Input.GetKeyDown("a"))
        {
            gameStart = false;
            StartGameCanvas.gameObject.SetActive(false);
        }
        if (gameOver == true && Input.GetKeyDown("a"))
        {
            gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
