using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public int maxLives = 3;
    int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            currentCanvas = FindObjectOfType<CanvasManager>();

            _score = value;
            //Debug.Log("Current Score Is: " + _score);
            currentCanvas.SetScoreText();

        }
    }

    int _lives;

    public int lives
    {
        get { return _lives; }
        set
        {
            currentCanvas = FindObjectOfType<CanvasManager>();

            if (_lives > value)
            {
                Respawn();
            }
            _lives = value;

            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
            else if (_lives < 0)
            {
                SceneManager.LoadScene("GameOver");
            }

            //Debug.Log("Current Lives Are: " + _lives);
            currentCanvas.SetLivesText();
            currentCanvas.SetScoreText();

        }
    }

    public GameObject playerInstance;
    public GameObject playerPrefab;
    public LevelManager currentLevel;

    CanvasManager currentCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        CameraFollow mainCamera = FindObjectOfType<CameraFollow>();

        if (mainCamera)
        {
            mainCamera.player = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
            playerInstance = mainCamera.player;
        }
        else
        {
            SpawnPlayer(spawnLocation);
        }

    }

    public void Respawn()
    {
        playerInstance.transform.position = currentLevel.spawnLocation.position;
    }
    public void GameOver()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            SceneManager.LoadScene("GameOver");
            Debug.Log("You died, press Esc key to go to Title Screen");
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
        _score = 0;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
