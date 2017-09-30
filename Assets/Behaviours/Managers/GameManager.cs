using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool restarting_scene;

    private static GameManager instance;


    void Awake()
    {
        if (instance == null)
        {
            InitSingleton();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void InitSingleton()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.StopAllSFX();
            SceneManager.LoadScene(0);
        }
    }


    void OnLevelWasLoaded(int _level)
    {

    }


    void OnApplicationQuit()
    {

    }

}
