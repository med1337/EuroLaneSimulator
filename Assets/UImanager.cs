using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public bool gameOver;
    public bool gameStart;
    public CanvasGroup GameOverCanvas;
    public CanvasGroup StartGameCanvas;
    
    [SerializeField]
    private Text money;
    [SerializeField]
    private Text highestscoreText;

    private int previousScore;
    private int currentScore;
    private int highestScore;


    // Use this for initialization
    void Start()
    {
        gameStart = true;
        gameOver = false;
        currentScore = 0;
        GameOverCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        checkForInput();
    }


    public void checkForInput()
    {
        if (Input.GetKeyDown("b"))
        {
            gameOver = true;
        }
        if (gameOver)
        {
            if (money != null)
            {
                money.text = GameManager.scene.money_panel.money.ToString();
                currentScore = GameManager.scene.money_panel.money;
                calculateHighestScore();
            }
            GameOverCanvas.gameObject.SetActive(true);

        }
        if (gameStart)
        {
            StartGameCanvas.gameObject.SetActive(true);
        }
        //start game
        if (gameStart && Input.GetButton("Submit"))
        {
            gameStart = false;
            currentScore = 0;
            StartGameCanvas.gameObject.SetActive(false);
            GameManager.scene.objective_manager.enabled = true;
        }
        if (gameOver && Input.GetButton("Submit"))
        {
            gameOver = false;
            GameManager.GameReset();
        }
    }
    public void calculateHighestScore()
    {
        if (currentScore >= highestScore)
        {
            highestScore = currentScore;
            highestscoreText.text = highestScore.ToString();
        }
    }
}
