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
        if (gameOver)
        {
            Debug.Log("UI Recognises Game Over");
            GameOverCanvas.gameObject.SetActive(true);
        }
        if (gameStart)
        {
            Debug.Log("UI calls game start.");
            StartGameCanvas.gameObject.SetActive(true);
        }
        checkForInput();
    }


    public void checkForInput()
    {
        //start game
        if (gameStart && Input.GetButton("Submit"))
        {
            gameStart = false;
            StartGameCanvas.gameObject.SetActive(false);
            GameManager.scene.objective_manager.enabled = true;
        }
        if (gameOver && Input.GetButton("Submit"))
        {
            gameOver = false;
            GameManager.GameReset();
        }
    }
}
