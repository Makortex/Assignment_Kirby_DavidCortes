using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CanvasManager : MonoBehaviour
{

    [Header("Buttons")]
    public Button startButton;
    public Button quitButton;
    public Button settingsButton;
    public Button backButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public Text livesText;
    public Text scoreText;

    public Text volSliderText;

    [Header("Images")]
    public Image[] hearts;

    [Header("Slider")]
    public Slider volSlider;

    [Header("Audio")]
    AudioSource pauseSoundAudio;
    public AudioClip pauseSound;
    public AudioMixerGroup soundFXMixer;

    // Start is called before the first frame update
    void Start()
    {
        if (startButton)
        {
            startButton.onClick.AddListener(() => GameManager.instance.StartGame());
        }

        if (settingsButton)
        {
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());
        }

        if (backButton)
        {
            backButton.onClick.AddListener(() => ShowMainMenu());
        }

        if (quitButton)
        {
            quitButton.onClick.AddListener(() => GameManager.instance.QuitGame());
        }

        if (returnToMenuButton)
        {
            returnToMenuButton.onClick.AddListener(() => GameManager.instance.ReturnToMenu());
        }

        if (returnToGameButton)
        {
            returnToGameButton.onClick.AddListener(() => ReturnToGame());
        }

        //if (GameManager.instance.lives > 0)
        //{
        //    SetLivesText();
        //    //GameManager.instance.GameOver();
        //}

        if (livesText)
        {
            SetLivesText();
        }

        if (scoreText)
        {
            SetScoreText();
        }
    }

    void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        Time.timeScale = 1;

    }

    public void SetLivesText()
    {
        if (GameManager.instance)
        {
            livesText.text = GameManager.instance.lives.ToString();
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < GameManager.instance.lives)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;

                }
            }
        }
        else
        {
            SetLivesText();
        }
    }
    public void SetScoreText()
    {
        if (GameManager.instance)
        {
            scoreText.text = GameManager.instance.score.ToString();
        }
        else
        {
            SetScoreText();
        }
    }

    void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    void ReturnToGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);

                if (!pauseSoundAudio)
                {
                    pauseSoundAudio = gameObject.AddComponent<AudioSource>();
                }
                if (pauseMenu.activeSelf)
                {
                    Time.timeScale = 0;
                    pauseSoundAudio.Play();
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }


        if (settingsMenu)
        {
            if (settingsMenu.activeSelf)
            {
                volSliderText.text = volSlider.value.ToString();
            }
        }
    }
}