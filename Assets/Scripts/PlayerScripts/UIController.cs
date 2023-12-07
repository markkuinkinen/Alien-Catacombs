using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    GameController gameController;
    GunController gunController;

    public GameObject PauseMenu;
    public GameObject deathMenu;
    public Button ResumeButton;
    public Button exitButton;
    public Button QuitButton;

    public string ZeroScore = "000000";
    public Text scoreText;
    public Text playerLevelText;
    public Text currencyAmountText;
    public Text deathText;
    public Text highscoreText;
    [SerializeField]
    static int highScore = 0;

    public bool isPaused;
    public bool playerIsPaused;
    public bool enemyIsPaused;
    public Slider playerHealthSlider;

    void Start()
    {
        gunController = FindObjectOfType<GunController>();
        gameController = FindObjectOfType<GameController>();
        playerHealthSlider.maxValue = gameController.maxPlayerHealth;
    }

    void Update()
    {
        trackLevelandScore();
        PauseGame();
        trackCurrency();
        TrackHealth();
        trackDeathText();
    }

    void trackDeathText()
    {
        deathText.text = "You salvaged " + gameController.currencyAmount + " crystals\n" + gameController.getTotalCrystals() + " crystals in total";

        if (gameController.totalExp > highScore)
        {
            highscoreText.text = "New High Score: " + gameController.totalExp.ToString();
        }
        else
        {
            highscoreText.text = "Score: " + gameController.totalExp.ToString();
        }
    }

    void TrackHealth()
    {
        playerHealthSlider.value = gameController.currentHp;
    }

    void trackLevelandScore()
    {
        playerLevelText.text = "Level " + gameController.playerLevel.ToString();

        if (gameController.totalExp < 100)
        {
            scoreText.text = "000000" + gameController.totalExp.ToString();
        }
        else if (gameController.totalExp < 1000)
        {
            scoreText.text = "0000" + gameController.totalExp.ToString();
        }
        else if (gameController.totalExp < 10000)
        {
            scoreText.text = "000" + gameController.totalExp.ToString();
        }
        else if (gameController.totalExp < 100000)
        {
            scoreText.text = "00" + gameController.totalExp.ToString();
        }
        else if (gameController.totalExp < 1000000)
        {
            scoreText.text = "0" + gameController.totalExp.ToString();
        }
        else
        {
            scoreText.text = gameController.totalExp.ToString();
        }
    }

    public void ActivateDeathMenu()
    {
        deathMenu.SetActive(true);
    }

    void trackCurrency()
    {
        currencyAmountText.text = ((int)gameController.currencyAmount).ToString();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && gameController.canPause)
        {
            PauseMenu.SetActive(true);
            playerIsPaused = true;
            enemyIsPaused = false;
            isPaused = true;
            gunController.isShooting = false;
            gunController.canShoot = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && gameController.canPause)
        {
            PauseMenu.SetActive(false);
            playerIsPaused = false;
            enemyIsPaused = false;
            isPaused = false;
            gunController.canShoot = true;
        }
    }

    public void ResumeGame()
    {
        playerIsPaused = false;
        PauseMenu.SetActive(false);
        isPaused = false;
        gunController.canShoot = true;
    }

    public void EnterStore()
    {
        SceneManager.LoadScene(2);
    }

    public void RestartGame()
    {
        if (gameController.totalExp > highScore)
        {
            highScore = (int)gameController.totalExp;
        }
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
