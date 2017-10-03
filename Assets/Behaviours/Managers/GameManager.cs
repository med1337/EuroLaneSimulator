using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static readonly int ROAD_SPEED_LIMIT = 35;
    public static bool restarting_scene;
    public static TempSceneRefs scene = new TempSceneRefs();
    public static List<Sprite> carSprites { get { return instance.carSprites_; } }
    public static GameObject car_prefab { get { return instance.car_prefab_; } }

    private static GameManager instance;

    [SerializeField] List<Sprite> carSprites_ = new List<Sprite>();
    [SerializeField] GameObject car_prefab_;

    private AudioSource player_horn;

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

        player_horn = GetComponent<AudioSource>();
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

        if (Input.GetKeyDown(KeyCode.H))
        {
            player_horn.Play();
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            player_horn.Stop();
        }

    }


    void OnLevelWasLoaded(int _level)
    {

    }


    void OnApplicationQuit()
    {

    }

}
